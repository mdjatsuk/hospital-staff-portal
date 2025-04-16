using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Domain;
using MVC_Project.Infra;

namespace MVC_Project.Soft.Controllers;

public abstract class BaseController<T>(DbContext context) : Controller where T : Entity
{
    private readonly Repo<T> _repo = new(context);

    public async Task<IActionResult> Index() => View(await _repo.GetAll());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var entity = await _repo.GetById(id);
        return entity == null ? NotFound() : View(entity);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(T entity)
    {
        if (!ModelState.IsValid) return View(entity);
        await _repo.Add(entity);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var entity = await _repo.GetById(id);
        return entity == null ? NotFound() : View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, T entity)
    {
        if (id != entity.Id) return NotFound();
        if (!ModelState.IsValid) return View(entity);
        await _repo.Update(entity);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var entity = await _repo.GetById(id);
        return entity == null ? NotFound() : View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}