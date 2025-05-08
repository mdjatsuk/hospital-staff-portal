using MVC.Data;

public sealed class MedicalRecordData : EntityData<MedicalRecordData>
{
    public int PatientId { get; set; }
    public int RecordNrId { get; set; }
    public DateTime? DiagnosedOn { get; set; }
    public string? PatientFullName { get; set; }
    public string? RecordNr { get; set; }
}

