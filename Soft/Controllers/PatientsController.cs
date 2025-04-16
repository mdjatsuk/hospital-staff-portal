using MVC_Project.Domain;
using MVC_Project.Soft.Data;

namespace MVC_Project.Soft.Controllers;
public class PatientsController(ApplicationDbContext c) : 
    BaseController<Patient>(c) { }