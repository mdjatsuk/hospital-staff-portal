using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

public sealed class AppointmentView : EntityView
{
    [Display(Name = dateName), Required, DataType(DataType.Date)] public DateTime? Date { get; set; }

    [Required, RegularExpression(locationEx, ErrorMessage = locationError)] public string? Location { get; set; }

    [Display(Name = appointName), Range(0, appointMaxFee, ErrorMessage = appointError)]
    public double? AppointmentFee { get; set; }
}