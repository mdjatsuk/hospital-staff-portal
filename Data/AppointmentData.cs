namespace MVC.Data;

public sealed class AppointmentData : EntityData<AppointmentData>
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime? Date { get; set; }
    public string? Room { get; set; }
    public double AppointmentFee { get; set; }
    public string? DoctorFullName { get; set; }
    public string? PatientFullName { get; set; }
}