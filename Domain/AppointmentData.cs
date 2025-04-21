using System.ComponentModel.DataAnnotations;

namespace MVC.Domain
{
    public class AppointmentData : Entity
    {
        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)] public DateTime? Date { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "Location must start with a capital letter.")]
        public string? Location { get; set; }

        [Display(Name = "Appointment Fee")]
        [Range(0, double.MaxValue, ErrorMessage = "Appointment Fee must be zero or positive.")]
        public double? AppointmentFee { get; set; }
    }
}
