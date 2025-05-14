using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Patient(PatientData? d) : Entity<PatientData>(d)
{
    public Patient() : this(null) { }
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public DateTime? DateOfBirth => data?.DateOfBirth;
    public Genders? Gender => data?.Gender;
    public string FullName => $"{FirstName} {LastName}";

    internal List<MedicalRecord> recordNr = [];
    public List<Diagnosis?> RecordNr => recordNr?
        .Where(r => r.RecordNr is not null)
        .Select(r => r.RecordNr)
        .ToList() ?? [];

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        recordNr.Clear();
        var entries = await (Services
            .Get<IMedicalRecordsRepo>()?
            .GetAsync(nameof(MedicalRecord.PatientId), Id ?? 0))!;
        foreach (var r in entries)
        {
            await r.LoadLazy();
            recordNr.Add(r);
        }
    }
}