using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Data
{
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

        public async Task Initialize(int itemsCount = 100, int listSize = 25)
        {
            count = itemsCount;
            size = listSize * 2;
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

                if (toGenerate <= 0 || existingCount >= toGenerate) return; // No need to seed

                var list = new List<TEntity>(size);

                // Generate names in batches for both patients and doctors
                var namesWithGenders = await ai.GenerateRandomNamesWithGendersAsync(toGenerate);

                var patientCount = 0;
                var doctorCount = 0;

                foreach (var nameWithGender in namesWithGenders)
                {
                    // Remove any trailing punctuation or spaces (like period)
                    var cleanedName = nameWithGender.FullName.Trim().TrimEnd('.', ',', ';', '!', '?');

                    var parts = cleanedName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    // Handle seeding for PatientData
                    if (patientCount < toGenerate && typeof(TEntity) == typeof(PatientData))
                    {
                        var gender = nameWithGender.Gender == 0 ? Genders.Male : Genders.Female;

                        var patient = new PatientData
                        {
                            FirstName = parts.ElementAtOrDefault(0) ?? "Name",
                            LastName = parts.ElementAtOrDefault(1) ?? "Surname",
                            Gender = gender
                        };

                        list.Add(patient as TEntity);
                        patientCount++;

                        // Save when we reach the batch size
                        if (list.Count >= size)
                        {
                            await save(set, list);
                        }
                    }
                    // Handle seeding for DoctorData
                    else if (doctorCount < toGenerate && typeof(TEntity) == typeof(DoctorData))
                    {
                        var doctor = new DoctorData
                        {
                            FirstName = parts.ElementAtOrDefault(0) ?? "Name",
                            LastName = parts.ElementAtOrDefault(1) ?? "Surname"
                        };

                        list.Add(doctor as TEntity);
                        doctorCount++;

                        // Save when we reach the batch size
                        if (list.Count >= size)
                        {
                            await save(set, list);
                        }
                    }

                    // If both patient and doctor counts have been met, we can stop
                    if (patientCount >= toGenerate && doctorCount >= toGenerate)
                    {
                        break;
                    }
                }

                // Save any leftovers
                await save(set, list);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding {typeof(TEntity).Name}: {ex.Message}");
                throw;
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
}
