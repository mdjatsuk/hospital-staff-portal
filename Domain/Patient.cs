using MVC.Core;
using MVC.Data;
using System.Numerics;

namespace MVC.Domain;

public sealed class Patient(PatientData? d) : Entity<PatientData>(d)
{
    public Patient() : this(null) { }
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public DateTime? DateOfBirth => data?.DateOfBirth;
    public Genders? Gender => data?.Gender;
    public string FullName => $"{FirstName} {LastName}";

    internal List<MedicalRecord> descriptions = [];
    public List<Medicine?> Descriptions => descriptions?
        .Where(r => r.Description is not null)
        .Select(r => r.Description)
        .ToList() ?? [];

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        descriptions.Clear();
        var entries = await (Services
            .Get<IMedicalRecordsRepo>()?
            .GetAsync(nameof(MedicalRecord.PatientId), Id ?? 0))!;
        foreach (var r in entries)
        {
            await r.LoadLazy();
            descriptions.Add(r);
        }
    }
}