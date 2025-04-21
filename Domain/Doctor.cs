using System.ComponentModel.DataAnnotations;

namespace MVC.Domain
{
    public class Doctor : Entity
    {
        [Display(Name = "First Name"), Required] public string? FirstName { get; set; }
        [Display(Name = "Last Name"), Required] public string? LastName { get; set; }
        [Display(Name = "Specialization"), Required] public string? Specialization { get; set; }

        [Display(Name = "Phone Number"), Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "The phone number must be exactly 8 digits.")]
        public string? PhoneNumber { get; set; }

    }
}
