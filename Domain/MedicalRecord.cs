using MVC.Core;
using MVC.Domain;
using System.Numerics;

public sealed class MedicalRecord(MedicalRecordData? d) : Entity<MedicalRecordData>(d)
{
    public MedicalRecord() : this(null) { }
    public int PatientId => data?.PatientId ?? 0;
    public int DiagnosisId => data?.DiagnosisId ?? 0;
    public DateTime? DiagnosedOn => data?.DiagnosedOn;
    public Patient? Patient => patient;
    public Diagnosis? Diagnosis => diagnosis;

    internal Patient? patient;
    internal Diagnosis? diagnosis;

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        var r = Services.Get<IPatientsRepo>();
        var p = Services.Get<IDiagnosesRepo>();
        if (p is null) return;
        if (r is null) return;
        patient = await r.GetAsync(PatientId)!;
        diagnosis = await p.GetAsync(DiagnosisId)!;
    }
}
