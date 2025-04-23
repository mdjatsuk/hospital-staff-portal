using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class DoctorsController(ApplicationDbContext c)
    : BaseController<Doctor, DoctorData, DoctorView>(c, new DoctorViewFactory(), d => new(d)) {}