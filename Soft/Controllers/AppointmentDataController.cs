using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class AppointmentDataController(ApplicationDbContext c) : BaseController<AppointmentData>(c) { }

