using System.ComponentModel.DataAnnotations;

namespace MVC.Aids.Attributes;

public class DateOfBirthValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date && date > DateTime.Today)
        {
            return new ValidationResult("Date of Birth cannot be in the future.");
        }

        return ValidationResult.Success;
    }
}