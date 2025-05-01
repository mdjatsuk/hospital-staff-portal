using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Soft.Data;
using System.Reflection;
using Microsoft.CodeAnalysis;
using System.Numerics;
using NuGet.Packaging.Signing;
using MVC.Aids.Attributes;

public class DbInitializer
{
    private readonly ApplicationDbContext? c;
    private readonly OpenAiService ai;
    private int count;
    private int batchSize;
    public DbInitializer(ApplicationDbContext? context, OpenAiService aiService)
    {
        c = context;
        ai = aiService;
    }

    public async Task Initialize(int itemsCount = 10)
    {
        if (c is null) return;

        count = itemsCount;
        batchSize = Math.Max(1, count / 4);

        try
        {
            c.Database.EnsureCreated();

            foreach (var set in GetDbSets())
            {
                var method = GetSeedMethod(set);
                if (method is not null)
                {
                    await (Task)method.Invoke(this, new[] { set })!;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database initialization: {ex.Message}");
            throw;
        }
    }

    private IEnumerable<object?> GetDbSets()
    {
        return c?.GetType()
                  .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                  .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                  .Select(p => p.GetValue(c))
                  .Where(v => v is not null)
               ?? Enumerable.Empty<object?>();
    }

    private MethodInfo? GetSeedMethod(object? set)
    {
        var t = set?.GetType().GetGenericArguments().FirstOrDefault();
        if (t == null) return null;

        return typeof(DbInitializer)
            .GetMethod(nameof(SeedData), BindingFlags.NonPublic | BindingFlags.Instance)?
            .MakeGenericMethod(t);
    }

    private async Task SeedData<TEntity>(DbSet<TEntity> set)
        where TEntity : EntityData, new()
    {
        try
        {
            var existingCount = set.Count();
            var toGenerate = count - existingCount;
            if (toGenerate <= 0) return;

            var list = new List<TEntity>(batchSize);

            if (typeof(TEntity) == typeof(PatientData) || typeof(TEntity) == typeof(DoctorData))
            {
                await SeedWithNames<TEntity>(set, toGenerate, list);
            }
            else if (typeof(TEntity) == typeof(DiagnosisData))
            {
                await SeedDiagnosisData<TEntity>(set, toGenerate, list);
            }
            else if (typeof(TEntity) == typeof(AppointmentData))
            {
                await SeedAppointmentData<TEntity>(set, toGenerate, list);
            }
            else if (typeof(TEntity) == typeof(MedicalRecord))
            {
                
            }
            else
            {
                foreach (var entity in GenerateGenericData<TEntity>(toGenerate))
                {
                    list.Add(entity);
                    if (list.Count >= batchSize)
                        await SaveBatch(set, list);
                }
            }

            await SaveBatch(set, list);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during seeding {typeof(TEntity).Name}: {ex.Message}");
            throw;
        }
    }

    private async Task SeedWithNames<TEntity>(DbSet<TEntity> set, int toGenerate, List<TEntity> list)
        where TEntity : EntityData, new()
    {
        var names = await ai.GenerateRandomNamesAndGendersAsync(toGenerate);
        foreach (var name in names)
        {
            if (list.Count >= count) break;

            var (first, last) = SplitName(name.fullName);
            var gender = name.gender;

            object? entity = CreateEntity<TEntity>(first, last, gender, null, Diagnoses.Unknown, null);

            if (entity is TEntity typedEntity)
            {
                list.Add(typedEntity);
            }

            if (list.Count >= batchSize)
                await SaveBatch(set, list);
        }
    }

    private async Task SeedDiagnosisData<TEntity>(DbSet<TEntity> set, int toGenerate, List<TEntity> list)
        where TEntity : EntityData, new()
    {
        var descriptionsWithDiagnoses = await ai.GenerateRandomDiagnosisDescriptionsAsync(toGenerate);

        foreach (var descriptionsWithDiagnosis in descriptionsWithDiagnoses)
        {
            if (list.Count >= count) break;

            var description = descriptionsWithDiagnosis.description;

            Diagnoses diagnosis = descriptionsWithDiagnosis.diagnosis;

            object? entity = CreateEntity<TEntity>(null, null, Genders.Unknown, description, diagnosis, null);

            if (entity is TEntity typedEntity)
            {
                list.Add(typedEntity);
            }

            if (list.Count >= batchSize)
            {
                await SaveBatch(set, list);
            }
        } 
        if (list.Any())
        {
            await SaveBatch(set, list);
        }
    }

    private async Task SeedAppointmentData<TEntity>(DbSet<TEntity> set, int toGenerate, List<TEntity> list) where TEntity : EntityData, new()
    {
        var rooms = await ai.GenerateRandomRoomsAsync(toGenerate);

        foreach (var room in rooms)
        {
            if (list.Count >= count) break;

            object? entity = CreateEntity<TEntity>(null, null, Genders.Unknown, null, Diagnoses.Unknown, room);

            if (entity is TEntity typedEntity)
            {
                list.Add(typedEntity);
            }

            if (list.Count >= batchSize)
            {
                await SaveBatch(set, list);
            }
        }
        if (list.Any())
        {
            await SaveBatch(set, list);
        }

    }


    private object? CreateEntity<TEntity>(string? firstName, string? lastName, Genders gender, string? description, Diagnoses diagnosis, string? room)
    {
        if (typeof(TEntity) == typeof(PatientData))
        {
            return new PatientData
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = MVC.Aids.Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now)
            };
        }
        if (typeof(TEntity) == typeof(DoctorData))
        {
            var random = new System.Random();
            var index = random.Next(0, EmailDomainProvider.EmailDomains.Count);
            var randomDomain = EmailDomainProvider.EmailDomains[index];

            return new DoctorData
            {
                FirstName = firstName,
                LastName = lastName,
                Specialization = (Specialities?)MVC.Aids.Random.EnumOf(typeof(Specialities)),
                PhoneNumber = MVC.Aids.Random.Int64(50000000, 59999999),
                EmailAddress = $"{firstName?.ToLower()}.{lastName?.ToLower()}@{randomDomain}"

            };
        }
        if (typeof(TEntity) == typeof(DiagnosisData))
        {
            return new DiagnosisData
            {
                DiagnosisName = diagnosis,
                Description = description,
                RequiresSurgery = MVC.Aids.Random.Boolean()
            };
        }
        if (typeof(TEntity) == typeof(AppointmentData))
        {
            var doctorIds = c?.Doctors.Select(d => d.Id).ToList();
            var patientIds = c?.Patients.Select(p => p.Id).ToList();

            if (doctorIds == null || doctorIds.Count == 0 || patientIds == null || patientIds.Count == 0)
                throw new InvalidOperationException("Doctors or Patients table is empty.");

            var doctorId = doctorIds[Random.Shared.Next(doctorIds.Count)];
            var patientId = patientIds[Random.Shared.Next(patientIds.Count)];

            var doctor = c.Doctors.FirstOrDefault(d => d.Id == doctorId);
            var patient = c.Patients.FirstOrDefault(p => p.Id == patientId);

            return new AppointmentData
            {
                DoctorId = doctorId,
                PatientId = patientId,
                Date = MVC.Aids.Random.DateTime(DateTime.Now, DateTime.Now.AddDays(30)),
                Room = room,
                AppointmentFee = Math.Round(MVC.Aids.Random.Double(50, 500), 2),
                DoctorFullName = doctor != null ? $"{doctor.FirstName} {doctor.LastName}" : "Unknown Doctor",
                PatientFullName = patient != null ? $"{patient.FirstName} {patient.LastName}" : "Unknown Patient"
            };
        }

        if (typeof(TEntity) == typeof(MedicalRecordData))
        {
            var descriptionIds = c?.Diagnoses.Select(d => d.Id).ToList();
            var patientIds = c?.Patients.Select(p => p.Id).ToList();

            if (descriptionIds == null || descriptionIds.Count == 0 || patientIds == null || patientIds.Count == 0)
                throw new InvalidOperationException("Doctors or Patients table is empty.");

            var descriptionId = descriptionIds[Random.Shared.Next(descriptionIds.Count)];
            var patientId = patientIds[Random.Shared.Next(patientIds.Count)];

            var descriptionNew = c.Diagnoses.FirstOrDefault(d => d.Id == descriptionId);
            var patient = c.Patients.FirstOrDefault(d => d.Id == patientId);

            return new MedicalRecordData
            {
                DescriptionId = descriptionId,
                PatientId = patientId,
                DiagnosedOn = MVC.Aids.Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now),
                Diagnos = (Diagnoses?)MVC.Aids.Random.EnumOf(typeof(Diagnoses)),
                DescriptionName = descriptionNew != null ? $"{ descriptionNew.Description}" : "Unknown Doctor",
                PatientFullName = patient != null ? $"{patient.FirstName} {patient.LastName}" : "Unknown Doctor"
            };
        }


        return null;
    }

    private static (string FirstName, string LastName) SplitName(string fullName)
    {
        var cleaned = fullName.Trim().TrimEnd('.', ',', ';', '!', '?');
        var parts = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return (parts.ElementAtOrDefault(0) ?? "Name", parts.ElementAtOrDefault(1) ?? "Surname");
    }

    private IEnumerable<TEntity> GenerateGenericData<TEntity>(int count)
        where TEntity : EntityData, new()
    {
        for (int i = 0; i < count; i++)
        {
            var obj = MVC.Aids.Random.Object<TEntity>();
            if (obj is null) continue;
            obj.Id = 0;

            var idProps = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                    p.Name != nameof(EntityData.Id) &&
                    p.Name.EndsWith("Id") &&
                    p.PropertyType == typeof(int));

            foreach (var prop in idProps)
            {
                var randomValue = MVC.Aids.Random.Int32(1, count);
                prop.SetValue(obj, randomValue);
            }

            yield return obj;
        }
    }

    private async Task SaveBatch<TEntity>(DbSet<TEntity> set, List<TEntity> list)
        where TEntity : EntityData
    {
        if (c is not null && list.Any())
        {
            set.AddRange(list);
            await c.SaveChangesAsync();
            list.Clear();
        }
    }
}
