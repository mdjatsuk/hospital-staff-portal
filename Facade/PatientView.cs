using MVC.Aids.Attributes;
using System.ComponentModel.DataAnnotations;
using MVC.Data;
using System.ComponentModel;

namespace MVC.Facade;

[DisplayName(patients)] public sealed class PatientView : EntityView
{
    [Display(Name = firstName), Required(ErrorMessage = requiredError), StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = firstNameError),
         RegularExpression(namesEx, ErrorMessage = firstNameLetterError)]
    public string? FirstName { get; set; }

    [Display(Name = lastName), Required(ErrorMessage = requiredError), StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = lastNameError),
        RegularExpression(namesEx, ErrorMessage = lastNameLetterError)]
    public string? LastName { get; set; }

    [Display(Name = birthName), DataType(DataType.Date), Required(ErrorMessage = requiredError), DateOfBirthValidation]
    public DateTime? DateOfBirth { get; set; }
    public Genders? Gender { get; set; }

}