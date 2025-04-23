using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class AppointmentsController(ApplicationDbContext c)
    : BaseController<Appointment, AppointmentData, AppointmentView>(c, new AppointmentViewFactory(), d => new (d)) {}

