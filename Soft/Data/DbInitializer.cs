using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Data;

public class DbInitializer
{
    private readonly ApplicationDbContext? c;
    private readonly OpenAiService ai;
    private int count;
    private int size;

    public DbInitializer(ApplicationDbContext? context, OpenAiService aiService)
    {
        c = context;
        ai = aiService;
    }

    public async Task Initialize(int itemsCount = 100)
    {
        count = itemsCount;
        size = itemsCount / 4;
        if (c is null) return;

        try
        {
            c.Database.EnsureCreated();

            foreach (var set in sets)
            {
                var method = methodInfo(set);
                if (method is null) continue;
                await (Task)method.Invoke(this, new[] { set })!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database initialization: {ex.Message}");
            throw;
        }
    }

    private MethodInfo? methodInfo(object? set)
    {
        var t = set?.GetType().GetGenericArguments().FirstOrDefault();
        if (t == null) return null;

        return typeof(DbInitializer)
            .GetMethod(nameof(seedData), BindingFlags.NonPublic | BindingFlags.Instance)!
            .MakeGenericMethod(t);
    }

    private IEnumerable<object?> sets
    {
        get
        {
            var t = c?.GetType();
            var props = t?.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.DeclaredOnly
            );
            var dbProps = props?.Where(p => p.PropertyType.IsGenericType &&
                                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
            var setObjects = dbProps?.Select(p => p.GetValue(c));
            var notNull = setObjects?.Where(p => p is not null);
            return notNull?.ToArray() ?? Array.Empty<object?>();
        }
    }

    private async Task seedData<TEntity>(DbSet<TEntity> set)
        where TEntity : EntityData, new()
    {
        try
        {
            var existingCount = set.Count();
            var toGenerate = count - existingCount;

            if (toGenerate <= 0) return;

            var list = new List<TEntity>(size);

            if (typeof(TEntity) == typeof(PatientData) || typeof(TEntity) == typeof(DoctorData))
            {
                var patientCount = 0;
                var doctorCount = 0;
                var namesWithGenders = await ai.GenerateRandomNamesWithGendersAsync(toGenerate);

                foreach (var nameWithGender in namesWithGenders)
                {
                    var cleanedName = nameWithGender.FullName.Trim().TrimEnd('.', ',', ';', '!', '?');
                    var parts = cleanedName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var firstName = parts.ElementAtOrDefault(0) ?? "Name";
                    var lastName = parts.ElementAtOrDefault(1) ?? "Surname";

                    if (patientCount < toGenerate && typeof(TEntity) == typeof(PatientData))
                    {
                        var gender = nameWithGender.Gender == 0 ? Genders.Male : Genders.Female;
                        var patient = new PatientData
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Gender = gender,
                            DateOfBirth = Aids.Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now)
                        };
                        list.Add(patient as TEntity);
                        patientCount++;
                    }
                    else if (doctorCount < toGenerate && typeof(TEntity) == typeof(DoctorData))
                    {
                        var doctor = new DoctorData
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Specialization = (Specialties?)Aids.Random.EnumOf(typeof(Specialties)),
                            PhoneNumber = Aids.Random.Int64(10000000, 99999999)
                        };
                        list.Add(doctor as TEntity);
                        doctorCount++;
                    }

                    if (list.Count >= size)
                    {
                        await save(set, list);
                    }
                }
            }
            else
            {
                foreach (var entity in getData<TEntity>(toGenerate))
                {
                    list.Add(entity);

                    if (list.Count >= size)
                    {
                        await save(set, list);
                    }
                }
            }

           
            await save(set, list);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during seeding {typeof(TEntity).Name}: {ex.Message}");
            throw;
        }
    }

    private IEnumerable<TEntity> getData<TEntity>(int toGenerate) where TEntity : EntityData, new()
    {
        if (typeof(TEntity) != typeof(PatientData) && typeof(TEntity) != typeof(DoctorData))
        {
            for (var i = 0; i < toGenerate; i++)
            {
                var d = Aids.Random.Object<TEntity>();
                if (d is null) continue;
                d.Id = 0;
                yield return d;
            }
        }
        
    }

    private async Task save<TEntity>(DbSet<TEntity> set, List<TEntity> list) where TEntity : EntityData, new()
    {
        if (c is not null && list.Any())
        {
            set.AddRange(list);
            await c.SaveChangesAsync();
            list.Clear();
        }
    }
}