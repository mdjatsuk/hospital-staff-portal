using MVC.Data;

namespace MVC.Domain;

public class Entity<TData>(TData? d) where TData : EntityData<TData>
{
    public TData? data { get; } = d?.Clone();
    public int? Id => data?.Id;
    public virtual async Task LoadLazy() => await Task.CompletedTask;
}