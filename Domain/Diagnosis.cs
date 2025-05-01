using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Diagnosis(DiagnosisData? d) : Entity<DiagnosisData>(d)
{
    public Diagnosis() : this(null) { }
    public Diagnoses? DiagnosisName => data?.DiagnosisName;
    public string? Description => data?.Description;
    public bool? RequiresSurgery => data?.RequiresSurgery;
}