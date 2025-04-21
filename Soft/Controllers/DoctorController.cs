using MVC_Project.Domain;
using MVC_Project.Soft.Data;

namespace MVC_Project.Soft.Controllers;

public class DoctorController(ApplicationDbContext c) :
BaseController<Doctor>(c) { }
