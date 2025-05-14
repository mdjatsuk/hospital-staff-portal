using MVC.Aids.Attributes;
using MVC.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MVC.Facade;

[DisplayName(doctors)] public sealed class DoctorView : EntityView
{
    [Display(Name = firstName), Required(ErrorMessage = requiredError), StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = firstNameError),
        RegularExpression(namesEx, ErrorMessage = firstNameLetterError)]
    public string? FirstName { get; set; }
    [Display(Name = lastName), Required(ErrorMessage = requiredError), StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = lastNameError),
        RegularExpression(namesEx, ErrorMessage = lastNameLetterError)]
    public string? LastName { get; set; }
    [Display(Name = specializationName), Required(ErrorMessage = requiredError)] public Specialities? Specialization { get; set; }

    [Display(Name = phoneNumberName), Required(ErrorMessage = requiredError), Range(50000000, 59999999, ErrorMessage = phoneNumberError)]
    public long? PhoneNumber { get; set; }

    [Display(Name = emailName), Required(ErrorMessage = requiredError), EmailValidation]
    public string? EmailAddress { get; set; }

}
