using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Soft.Data;
using NuGet.Packaging.Signing;
using System.Reflection;

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
                await SeedSpecialEntities<TEntity>(set, toGenerate, list);
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

    private async Task SeedSpecialEntities<TEntity>(DbSet<TEntity> set, int toGenerate, List<TEntity> list)
        where TEntity : EntityData, new()
    {
        var names = await ai.GenerateRandomNamesWithGendersAsync(toGenerate);

        foreach (var name in names)
        {
            var (first, last) = SplitName(name.FullName);
            var gender = name.Gender == 0 ? Genders.Male : Genders.Female;

            object? entity = typeof(TEntity) switch
            {
                var t when t == typeof(PatientData) => new PatientData
                {
                    FirstName = first,
                    LastName = last,
                    Gender = gender,
                    DateOfBirth = MVC.Aids.Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now)
                },
                var t when t == typeof(DoctorData) => new DoctorData
                {
                    FirstName = first,
                    LastName = last,
                    Specialization = (Specialties?)MVC.Aids.Random.EnumOf(typeof(Specialties)),
                    PhoneNumber = MVC.Aids.Random.Int64(10000000, 99999999)
                },
                _ => null
            };

            if (entity is TEntity typedEntity)
                list.Add(typedEntity);

            if (list.Count >= batchSize)
                await SaveBatch(set, list);
        }
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
