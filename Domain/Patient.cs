using System.ComponentModel.DataAnnotations;
using MVC.Aids.Attributes;

namespace MVC.Domain
{
    public class Patient : Entity
    {
        [Display(Name = "First Name"), Required, StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name"), Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters.")]
        public string? LastName { get; set; }

        [Display(Name = "Date Of Birth"), DataType(DataType.Date), Required, DateOfBirthValidation]
        public DateTime? DateOfBirth { get; set; }
        public enum Genders { Male = 0, Female = 1 }
        public Genders? Gender { get; set; }
    }
}
