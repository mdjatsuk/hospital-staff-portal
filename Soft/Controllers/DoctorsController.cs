using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class DoctorsController(ApplicationDbContext c) :
BaseController<Doctor>(c)
{ }
