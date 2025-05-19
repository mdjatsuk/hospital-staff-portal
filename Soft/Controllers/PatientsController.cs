using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;
public sealed class PatientsController(ApplicationDbContext c)
    : BaseController<Patient, PatientData, PatientView>(c, new PatientViewFactory(), d => new(d))
{ }