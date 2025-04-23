using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class DiagnosesController(ApplicationDbContext c)
    : BaseController<Diagnosis, DiagnosisData, DiagnosisView>(c, new DiagnosisViewFactory(), d => new(d)) {}