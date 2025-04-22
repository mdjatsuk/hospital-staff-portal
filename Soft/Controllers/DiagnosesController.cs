using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class DiagnosesController(ApplicationDbContext c) : BaseController<Diagnosis>(c) { }