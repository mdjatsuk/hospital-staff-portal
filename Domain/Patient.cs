using MVC.Core;
using MVC.Data;
using System.Numerics;

namespace MVC.Domain;

public class Patient(PatientData d) : Entity<PatientData>(d)
{
    public int DiagnosisId => data?.DiagnosisId ?? 0;
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public DateTime? DateOfBirth => data?.DateOfBirth;
    public Genders? Gender => data?.Gender;
    public string FullName => $"{FirstName} {LastName}";

    public Diagnosis? Diagnosis => diagnosis;

    internal Diagnosis? diagnosis;

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        diagnosis = await Services.Get<IDiagnosesRepo>()?.GetAsync(DiagnosisId)!;
    }
}