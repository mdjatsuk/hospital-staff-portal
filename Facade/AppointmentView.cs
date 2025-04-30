using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Facade;

[DisplayName(appointments)] public sealed class AppointmentView : EntityView
{
    [Display(Name = dateName), Required, DataType(DataType.Date)] public DateTime? Date { get; set; }
    [Display(Name = doctor)] public int DoctorId { get; set; }
    [Display(Name = patient)] public int PatientId { get; set; }
    [Display(Name = doctor)] public string? Doctor { get; set; }
    [Display(Name = patient)] public string? Patient { get; set; }
    [Required, RegularExpression(locationEx, ErrorMessage = locationError)] public string? Location { get; set; }

    [Display(Name = appointName), Range(appointMinFee, appointMaxFee, ErrorMessage = appointError)]
    public double? AppointmentFee { get; set; }
}