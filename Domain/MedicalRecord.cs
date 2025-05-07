using MVC.Core;
using MVC.Data;
using MVC.Domain;
using System.Numerics;

public sealed class MedicalRecord(MedicalRecordData? d) : Entity<MedicalRecordData>(d)
{
    public MedicalRecord() : this(null) { }
    public int PatientId => data?.PatientId ?? 0;
    public int DescriptionId => data?.DescriptionId ?? 0;
    public DateTime? DiagnosedOn => data?.DiagnosedOn;
    public Diagnoses? Diagnosis => data?.Diagnosis;
    public Patient? Patient => patient;
    public Diagnosis? Description => description;

    internal Patient? patient;
    internal Diagnosis? description;
    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        patient = await getItem<IPatientsRepo,Patient>(PatientId)!;
        description = await getItem<IDiagnosesRepo,Diagnosis>(DescriptionId)!;
    }
}
