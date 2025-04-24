using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MVC.Facade;

[DisplayName("Doctors")] public sealed class DoctorView : EntityView
{
    [Display(Name = firstName), Required, StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = firstNameError)]
    public string? FirstName { get; set; }
    [Display(Name = lastName), Required, StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = firstNameError)]
    public string? LastName { get; set; }
    [Display(Name = specializationName), Required] public string? Specialization { get; set; }

    [Display(Name = phoneNumberName), Required, RegularExpression(phoneNumberEx, ErrorMessage = phoneNumberError)]
    public string? PhoneNumber { get; set; }

}
