using MVC.Core;
using MVC.Domain;

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
        patient = await Services.Get<IPatientsRepo>()?.GetAsync(PatientId)!;
        diagnosis = await Services.Get<IDiagnosesRepo>()?.GetAsync(DiagnosisId)!;
    }
}
