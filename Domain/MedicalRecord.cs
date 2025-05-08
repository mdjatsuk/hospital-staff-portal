using MVC.Core;
using MVC.Data;
using MVC.Domain;
using System.Numerics;

public sealed class MedicalRecord(MedicalRecordData? d) : Entity<MedicalRecordData>(d)
{
    public MedicalRecord() : this(null) { }
    public int PatientId => data?.PatientId ?? 0;
    public int DescriptionId => data?.DescriptionId ?? 0;
    public int DiagnosisId => data?.DiagnosisId ?? 0;
    public DateTime? DiagnosedOn => data?.DiagnosedOn;
    public Patient? Patient => patient;
    public Medicine? Description => description;
    public Medicine? Diagnosis => diagnosis;

    internal Patient? patient;
    internal Medicine? description;
    internal Medicine? diagnosis;
    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        patient = await getItem<IPatientsRepo,Patient>(PatientId)!;
        description = await getItem<IMedicinesRepo,Medicine>(DescriptionId)!;
        diagnosis =await getItem<IMedicinesRepo,Medicine>(DiagnosisId)!;
    }
}
