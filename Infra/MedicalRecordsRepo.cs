using Microsoft.EntityFrameworkCore;
using MVC.Domain;

namespace MVC.Infra;

public sealed class MedicalRecordsRepo(DbContext db)
    : Repo<MedicalRecord, MedicalRecordData>(db, d => new(d)), IMedicalRecordsRepo
{ }
