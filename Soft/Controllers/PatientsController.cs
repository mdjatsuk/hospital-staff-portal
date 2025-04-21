using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;
public class PatientsController(ApplicationDbContext c) :
    BaseController<Patient>(c)
{ }