using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class AppointmentData : EntityData<AppointmentData>
{
    public int DiagnosisNameId { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public double AppointmentFee { get; set; }
}