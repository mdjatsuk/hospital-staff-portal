using MVC.Data;
using MVC.Soft.Data.Seeding;
using MVC.Soft.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly OpenAiService _openAi;

    public DbInitializer(ApplicationDbContext context, OpenAiService openAi)
    {
        _context = context;
        _openAi = openAi;
    }

    public async Task Initialize(int count)
    {
        await _context.Database.MigrateAsync();

        await Seed<PatientData>(count);
        await Seed<DoctorData>(count);
        await Seed<DiagnosisData>(count);
        await Seed<AppointmentData>(count);
        await Seed<MedicalRecordData>(count);
    }

    private async Task Seed<TEntity>(int count) where TEntity : class, new()
    {
        if (await _context.Set<TEntity>().AnyAsync()) return;

        var config = GenerationConfigs.Get<TEntity>(_openAi);
        var openAiData = config.OpenAiGenerator is not null
            ? await config.OpenAiGenerator(count)
            : new List<Dictionary<string, object>>();

        var entities = new List<TEntity>();

        for (int i = 0; i < count; i++)
        {
            var entity = new TEntity();
            var row = i < openAiData.Count ? openAiData[i] : null;

            var referenceValues = await GetReferenceValues<TEntity>();

            foreach (var rule in config.PropertyRules)
            {
                var prop = typeof(TEntity).GetProperty(rule.PropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (prop == null || !prop.CanWrite) continue;

                object? value = rule.GeneratorType switch
                {
                    GeneratorType.Random => rule.RandomGenerator?.Invoke(),
                    GeneratorType.OpenAi => row != null && row.TryGetValue(rule.PropertyName, out var v) ? v : null,
                    GeneratorType.ReferenceId => referenceValues.TryGetValue(rule.PropertyName, out var val) ? val : null,
                    _ => null
                };

                if (value != null)
                    prop.SetValue(entity, value);
            }

            entities.Add(entity);
        }

        _context.Set<TEntity>().AddRange(entities);
        await _context.SaveChangesAsync();
    }


    private static HashSet<(int DoctorId, int PatientId)> usedAppointmentPairs = new ();
    private static HashSet<(int DiagnosisId, int PatientId)> usedMedicalRecordPairs = new();


    private async Task<Dictionary<string, object>> GetReferenceValues<TEntity>()
    {
        var result = new Dictionary<string, object>();

        if (typeof(TEntity) == typeof(AppointmentData))
        {
            var doctor = await _context.Doctors.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            var patient = await _context.Patients.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();

            while (doctor != null && patient != null && usedAppointmentPairs.Contains((doctor.Id, patient.Id)))
            {
                doctor = await _context.Doctors.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
                patient = await _context.Patients.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            }

            if (doctor != null && patient != null)
            {
                result["DoctorId"] = doctor.Id;
                result["PatientId"] = patient.Id;
                result["DoctorFullName"] = $"{doctor.FirstName} {doctor.LastName}";
                result["PatientFullName"] = $"{patient.FirstName} {patient.LastName}";
                usedAppointmentPairs.Add((doctor.Id, patient.Id));
            }
        }
        else if (typeof(TEntity) == typeof(MedicalRecordData))
        {
            var record = await _context.Diagnoses.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            var patient = await _context.Patients.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();

            while (record != null && patient != null && usedMedicalRecordPairs.Contains((record.Id, patient.Id)))
            {
                record = await _context.Diagnoses.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
                patient = await _context.Patients.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            }

            if (record != null && patient != null)
            {
                result["RecordNrId"] = record.Id;
                result["RecordNr"] = record.RecordNr;
                result["PatientId"] = patient.Id;
                result["PatientFullName"] = $"{patient.FirstName} {patient.LastName}";
            }
        }

        return result;
    }
}
