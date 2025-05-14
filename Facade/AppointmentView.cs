using MVC.Aids.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(appointments)] public sealed class AppointmentView : EntityView
{
    [Display(Name = dateName), Required(ErrorMessage = requiredError), DataType(DataType.Date), DateInFutureValidation] 
    public DateTime? Date { get; set; }
    [Display(Name = doctor)] public int DoctorId { get; set; }
    [Display(Name = patient)] public int PatientId { get; set; }
    [Display(Name = doctor)] public string? DoctorFullName { get; set; }
    [Display(Name = patient)] public string? PatientFullName { get; set; }
    [Required, RegularExpression(roomEx, ErrorMessage = roomError)] public string? Room { get; set; }

    [Display(Name = appointName), Range(appointMinFee, appointMaxFee, ErrorMessage = appointError)]
    public double? AppointmentFee { get; set; }
}