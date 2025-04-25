using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Soft.Data;

namespace Mvc.Soft.Data;

public class DbInitializer(ApplicationDbContext? c, OpenAiService ai)
{
    private int count;
    private int size;

    public async Task Initialize(int itemsCount = 1000, int listSize = 250)
    {
        count = itemsCount;
        size = listSize;
        if (c is null) return;
        c.Database.EnsureCreated();
        foreach (var set in sets)
        {
            var method = methodInfo(set);
            if (method is null) continue;
            await (Task)method.Invoke(this, new[] { set })!;
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
            var result = notNull?.ToArray() ?? Array.Empty<object?>();
            return result;
        }
    }

    private async Task seedData<TEntity>(DbSet<TEntity> set)
       where TEntity : EntityData, new()
    {
        var cnt = set.Count();
        var list = new List<TEntity>(size);
        var toGenerate = count - cnt;

        if (typeof(TEntity) == typeof(PatientData))
        {
            for (int i = 0; i < toGenerate; i++)
            {
                var name = await ai.GenerateRandomNameAsync();
                var parts = name.Split(' ');

                var patientData = new PatientData
                {
                    FirstName = parts.ElementAtOrDefault(0) ?? "Name",
                    LastName = parts.ElementAtOrDefault(1) ?? "Surname"
                };

                // Directly add PatientData to the DbSet
                list.Add(patientData as TEntity);
                if (list.Count >= size)
                {
                    await save(set, list);
                }
            }
        }

        await save(set, list);
    }

    private async Task save<TEntity>(DbSet<TEntity> set, List<TEntity> list) where TEntity : EntityData, new()
    {
        if (c is not null)
        {
            set.AddRange(list);
            await c.SaveChangesAsync();
        }
        list.Clear();
    }
}
