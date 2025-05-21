using MVC.Data;
using MVC.Soft.Data.Seeding;
using MVC.Soft.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVC.Domain;
using Random = MVC.Aids.Random;

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
        var existingCount = await _context.Set<TEntity>().CountAsync();
        if (existingCount >= count) return;

        int toGenerate = count - existingCount;

        var config = GenerationConfigs.Get<TEntity>(_openAi);
        var openAiData = config.OpenAiGenerator is not null
            ? await config.OpenAiGenerator(toGenerate)
            : new List<Dictionary<string, object>>();

        var entities = new List<TEntity>();

        for (int i = 0; i < toGenerate; i++)
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
    private static HashSet<int> usedDiagnoses = new();
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
            var availableDiagnosisIds = await _context.Diagnoses
                .Select(d => d.Id)
                .Where(id => !usedDiagnoses.Contains(id))
                .ToListAsync();

            if (availableDiagnosisIds.Count == 0)
                return result;

            int randomIndex = Random.Int32(0, availableDiagnosisIds.Count - 1);
            int selectedDiagnosisId = availableDiagnosisIds[randomIndex];

            var diagnosis = await _context.Diagnoses
                .FirstOrDefaultAsync(d => d.Id == selectedDiagnosisId);

            var patient = await _context.Patients
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();

            if (diagnosis != null && patient != null)
            {
                result["RecordNrId"] = diagnosis.Id;
                result["RecordNr"] = diagnosis.RecordNr;
                result["PatientId"] = patient.Id;
                result["PatientFullName"] = $"{patient.FirstName} {patient.LastName}";
                usedDiagnoses.Add(diagnosis.Id);
            }
        }

        return result;
    }
}
