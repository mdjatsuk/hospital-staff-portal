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

        public async Task Initialize(int itemsCount = 1000, int listSize = 250)
        {
            count = itemsCount;
            size = listSize;
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

                if (typeof(TEntity) == typeof(PatientData))
                {
                    var list = new List<TEntity>(size);

                    // Generate all needed names in batches
                    var names = await ai.GenerateRandomNamesAsync(toGenerate);

                    foreach (var name in names)
                    {
                        // Remove any trailing punctuation or spaces (like period)
                        var cleanedName = name.Trim().TrimEnd('.', ',', ';', '!', '?');

                        var parts = cleanedName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        var patient = new PatientData
                        {
                            FirstName = parts.ElementAtOrDefault(0) ?? "Name",
                            LastName = parts.ElementAtOrDefault(1) ?? "Surname",
                        };

                        list.Add(patient as TEntity);

                        if (list.Count >= size)
                        {
                            await save(set, list);
                        }
                    }

                    await save(set, list); // Save any leftovers
                }

                Console.WriteLine($"Total records in {typeof(TEntity).Name}: {set.Count()}");
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
