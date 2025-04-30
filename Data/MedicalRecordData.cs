using MVC.Data;

public class MedicalRecordData : EntityData<MedicalRecordData>
{
    public int PatientId { get; set; }
    public int DiagnosisId { get; set; }
    public DateTime? DiagnosedOn { get; set; }
}

