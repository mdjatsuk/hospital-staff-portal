using MVC.Data;

namespace MVC.Domain;

public class Diagnosis(DiagnosisData d) : Entity<DiagnosisData>(d)
{
    public string? DiagnosisName => data?.DiagnosisName;
    public string? Description => data?.Description;
    public bool? RequiresSurgery => data?.RequiresSurgery;
}