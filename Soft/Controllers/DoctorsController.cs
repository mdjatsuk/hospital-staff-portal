using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Infra;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public sealed class DoctorsController(ApplicationDbContext c)
    : BaseController<Doctor, DoctorData, DoctorView>(c, new DoctorViewFactory(), d => new(d)) 
{
    public IActionResult SelectItems(string researchString, string id)
    {
        var doctors = (r as DoctorsRepo)?.GetDoctors(researchString) ?? Enumerable.Empty<Doctor>();
        int.TryParse(id, out var parsedId);
        var result = doctors.Select(d => new {
            value = d.Id,
            text = d.FullName,
            selected = d.Id == parsedId
        });
        return Json(result);
    }
}