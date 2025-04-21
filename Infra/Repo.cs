using Microsoft.EntityFrameworkCore;
using MVC.Domain;

namespace MVC.Infra;

public class Repo<T>(DbContext c) where T : Entity
{
    private readonly DbContext db = c;
    private DbSet<T> Set => db.Set<T>();

    public async Task<List<T>> GetAll() => await Set.ToListAsync();

    public async Task<T?> GetById(int? id) => await Set.FindAsync(id);

    public async Task<T> Add(T entity)
    {
        Set.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(int? id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            Set.Remove(entity);
            await db.SaveChangesAsync();
        }
    }
}