using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Facade;

[DisplayName("Appointments")] public sealed class AppointmentView : EntityView
{
    [Display(Name = "Diagnosis")] public int DiagnosisNameId { get; set; }
    [Display(Name = dateName), Required, DataType(DataType.Date)] public DateTime? Date { get; set; }
    [Display(Name = "Doctor")] public int DoctorId { get; set; }
    [Display(Name = "Patient")] public int PatientId { get; set; }
    [Display(Name = "Doctor")] public string? Doctor { get; set; }
    [Display(Name = "Patient")] public string? Patient { get; set; }
    [Required, RegularExpression(locationEx, ErrorMessage = locationError)] public string? Location { get; set; }

    [Display(Name = appointName), Range(0, appointMaxFee, ErrorMessage = appointError)]
    public double? AppointmentFee { get; set; }
}