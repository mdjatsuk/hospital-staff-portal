using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;

namespace MVC.Infra;

public sealed class PatientsRepo(DbContext db)
    : Repo<Patient, PatientData>(db, d => new(d)), IPatientsRepo
{ }
