using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;

namespace MVC.Infra;

public sealed class AppointmentsRepo(DbContext db)
    : Repo<Appointment, AppointmentData>(db, d => new(d)), IAppointmentsRepo
{ }
