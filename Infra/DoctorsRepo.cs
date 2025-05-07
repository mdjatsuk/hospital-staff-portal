using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;

namespace MVC.Infra;

public sealed class DoctorsRepo(DbContext db)
    : Repo<Doctor, DoctorData>(db, d => new(d)), IDoctorsRepo
{ }
