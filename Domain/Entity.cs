using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public interface IEntity
{
    public int? Id { get; }
    public Task LoadLazy();
}
public class Entity<TData>(TData? d) : IEntity where TData : EntityData<TData>
{
    public TData? data { get; } = d?.Clone();
    public int? Id => data?.Id;
    public virtual async Task LoadLazy() => await Task.CompletedTask;
    protected async Task<TItem?> getItem<TRepo, TItem>(int id)
      where TRepo : IRepo<TItem> where TItem : class, IEntity
    {
        var r = Services.Get<TRepo>();
        if (r is null) return null;
        return await r.GetAsync(id);
    }
}