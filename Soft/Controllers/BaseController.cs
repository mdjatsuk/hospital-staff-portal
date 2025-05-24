using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Infra;

namespace MVC.Soft.Controllers;

[Authorize]
public abstract class BaseController<TObject, TData, TView>(DbContext c,
    AbstractViewFactory<TData, TView> f, Func<TData?, TObject> createObject) : Controller
    where TObject : Entity<TData> where TData : EntityData<TData>, new() where TView : EntityView, new()
{
    private const byte pageSize = 10;
    protected readonly Repo<TObject, TData> r = new(c, createObject);
    protected readonly AbstractViewFactory<TData, TView> f = f;
    protected readonly Func<TData?, TObject> createObject = createObject;


    private async Task<IActionResult> showAsync(string? viewName, int? id)
    {
        var o = await r.GetAsync(id);
        var v = await f.CreateView(o?.data, true);
        return (o == null) ? NotFound() : View(viewName, v);
    }
    public async Task<IActionResult> Index(int pageIdx = 0, string? orderBy = null,
     string? filter = null, int? selectedId = null)
    {
        ViewBag.PageIdx = pageIdx;
        ViewBag.PageCount = await r.PageCount(pageSize, filter);
        ViewBag.OrderBy = orderBy;
        ViewBag.Filter = filter;
        ViewBag.SelectedId = selectedId;
        return View((await r.GetAsync(pageIdx, pageSize, orderBy, filter))
            .Select(x => f.CreateView(x?.data)));
    }
    public async Task<IActionResult> Details(int? id) => await showAsync(nameof(Details), id);
    public IActionResult Create() => View(new TView());
    [HttpPost, ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Create(TView v)
    {
        if (!ModelState.IsValid) return View(v);
        var d = f.CreateData(v);
        await r.AddAsync(createObject(d));
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int? id) => await showAsync(nameof(Edit), id);
    [HttpPost, ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Edit(int id, TView v)
    {
        if (id != v.Id) return NotFound();
        if (!ModelState.IsValid) return View(v);
        var d = f.CreateData(v);
        await r.UpdateAsync(createObject(d));
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) => await showAsync(nameof(Delete), id);
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await r.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> SelectItems(string searchString, int id)
    {
        return Ok(await r.SelectItems(searchString, id));
    }
}