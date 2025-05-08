using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Diagnosis(DiagnosisData? d) : Entity<DiagnosisData>(d)
{
    public Diagnosis() : this(null) { }
    public string? RecordNr => data?.RecordNr;
    public string? DiagnosisName => data?.Diagnosis;
    public string? MedicineName => data?.Medicine;
    public string? Description => data?.Description;
    public bool? RequiresPrescription => data?.RequiresPrescription;
}