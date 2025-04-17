using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Soft.Models
{
    public class AppointmentData
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "Location must start with a capital letter.")]
        public string? Location { get; set; }

        [Display(Name = "Appointment Fee")]
        [Range(0,double.MaxValue, ErrorMessage = "Appointment Fee must be zero or positive.")]
        public double? AppointmentFee { get; set; }  
    }
}
