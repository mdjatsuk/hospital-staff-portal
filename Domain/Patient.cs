using MVC.Core;
using MVC.Data;
using System.Numerics;

namespace MVC.Domain;

public class Patient(PatientData d) : Entity<PatientData>(d)
{
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public DateTime? DateOfBirth => data?.DateOfBirth;
    public Genders? Gender => data?.Gender;
    public string FullName => $"{FirstName} {LastName}";

    internal List<MedicalRecord> diagnoses = [];
    public List<Diagnosis?> Diagnoses => diagnoses?
        .Where(r => r.Diagnosis is not null)
        .Select(r => r.Diagnosis)
        .ToList() ?? [];

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        diagnoses.Clear();
        var entries = await (Services
            .Get<IMedicalRecords>()?
            .GetAsync(nameof(MedicalRecord.PatientId), Id ?? 0))!;
        foreach (var r in entries)
        {
            await r.LoadLazy();
            diagnoses.Add(r);
        }
    }
}