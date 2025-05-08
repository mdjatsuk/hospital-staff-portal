using MVC.Core;
using MVC.Data;
using MVC.Domain;
using System.Numerics;

public sealed class MedicalRecord(MedicalRecordData? d) : Entity<MedicalRecordData>(d)
{
    public MedicalRecord() : this(null) { }
    public int PatientId => data?.PatientId ?? 0;

    public int RecordNrId => data?.RecordNrId ?? 0;
    public DateTime? DiagnosedOn => data?.DiagnosedOn;
    public Patient? Patient => patient;
    public Diagnosis? RecordNr => recordNr;

    internal Patient? patient;
    internal Diagnosis? recordNr;
    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        patient = await getItem<IPatientsRepo,Patient>(PatientId)!;
        recordNr = await getItem<IMedicinesRepo, Diagnosis>(RecordNrId)!;
    }
}
