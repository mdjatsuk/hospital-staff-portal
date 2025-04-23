using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Soft.Data;
namespace Mvc.Soft.Data;

public class DbInitializer(ApplicationDbContext? c)
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
            await (Task)method.Invoke(this, [set])!;
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
            var result = notNull?.ToArray() ?? [];
            return result;
        }
    }
    private async Task seedData<TEntity>(DbSet<TEntity> set)
       where TEntity : EntityData, new()
    {
        var cnt = set.Count();
        var list = new List<TEntity>(size);
        foreach (var d in getData<TEntity>(count - cnt))
        {
            list.Add(d);
            Thread.Sleep(1);
            if (list.Count < size) continue;
            await save(set, list);
        }
        await save(set, list);
    }
    private IEnumerable<TEntity> getData<TEntity>(int cnt) where TEntity : EntityData, new()
    {
        for (var i = 0; i < cnt; i++)
        {
            var d = MVC.Aids.Random.Object<TEntity>();
            if (d is null) continue;
            d.Id = 0;
            yield return d;
        }
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

