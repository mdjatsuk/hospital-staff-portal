using MVC.Aids.Attributes;
using System.ComponentModel.DataAnnotations;
using MVC.Data;
using System.ComponentModel;

namespace MVC.Facade;

[DisplayName(patients)] public sealed class PatientView : EntityView
{
    [Display(Name = firstName), Required, StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = firstNameError)]
    public string? FirstName { get; set; }

    [Display(Name = lastName), Required, StringLength(nameLength, MinimumLength = minNameLength, ErrorMessage = lastNameError)]
    public string? LastName { get; set; }

    [Display(Name = birthName), DataType(DataType.Date), Required, DateOfBirthValidation]
    public DateTime? DateOfBirth { get; set; }
    public Genders? Gender { get; set; }

}