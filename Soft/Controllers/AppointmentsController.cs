using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class AppointmentsController(ApplicationDbContext c) : BaseController<Appointment>(c) { }

