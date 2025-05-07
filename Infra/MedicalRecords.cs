using Microsoft.EntityFrameworkCore;
using MVC.Domain;

namespace MVC.Infra;

public sealed class MedicalRecords(DbContext db)
    : Repo<MedicalRecord, MedicalRecordData>(db, d => new(d)), IMedicalRecords
{ }
