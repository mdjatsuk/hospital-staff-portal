using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;

namespace MVC.Infra;

public sealed class DiagnosisRepo(DbContext db)
    : Repo<Diagnosis, DiagnosisData>(db, d => new(d)), IDiagnosisRepo
{ }
