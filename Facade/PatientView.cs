using MVC.Aids.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade
{
    public sealed class PatientView : EntityView
    {
        [Display(Name = firstName), Required, StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = firstNameError)]
        public string? FirstName { get; set; }

        [Display(Name = lastName), Required, StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = lastNameError)]
        public string? LastName { get; set; }

        [Display(Name = birthName), DataType(DataType.Date), Required, DateOfBirthValidation]
        public DateTime? DateOfBirth { get; set; }
        public enum Genders { Male = male, Female = female }
        public Genders? Gender { get; set; }
    }
}
