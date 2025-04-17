using MVC_Project.Domain;
using MVC_Project.Soft.Data;

namespace MVC_Project.Soft.Controllers;

public class AppointmentDatasController(ApplicationDbContext c) : BaseController<AppointmentData>(c) { }

