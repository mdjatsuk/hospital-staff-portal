using MVC.Data;

public class MedicalRecordData : EntityData<MedicalRecordData>
{
    public int PatientId { get; set; }
    public int DescriptionId { get; set; }
    public DateTime? DiagnosedOn { get; set; }
    public Diagnoses? Diagnos { get; set; }
    public string? DescriptionName { get; set; }
    public string? PatientFullName { get; set; }
}

