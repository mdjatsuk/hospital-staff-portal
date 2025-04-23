using MVC.Aids;
using MVC.Data;
using MVC.Facade;

public abstract class AbstractViewFactory<TData, TView>
    where TData : EntityData, new()
    where TView : EntityView, new()
{
    public virtual TView CreateView(TData? d)
    {
        var v = new TView();
        if (d is not null) Copy.Members(d, v);
        return v;
    }
    public virtual async Task<TView> CreateView(TData? d, bool loadLazy = false)
    {
        await Task.CompletedTask;
        return CreateView(d);
    }
    public virtual TData CreateData(TView? v)
    {
        var d = new TData();
        if (v is not null) Copy.Members(v, d);
        return d;
    }
}