using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Diagnosis(DiagnosisData? d) : Entity<DiagnosisData>(d)
{
    public Diagnosis() : this(null) { }
    public string? MedicineName => data?.MedicineName;
    public string? Description => data?.Description;
    public bool? RequiresSurgery => data?.RequiresSurgery;
    public bool? RequiresPrescription => data?.RequiresPrescription;
}