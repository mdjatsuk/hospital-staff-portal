using System.ComponentModel.DataAnnotations;

namespace MVC.Aids.Attributes
{
    public class EmailValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string email || string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("Email is required.");
            }

            if (!email.Contains("@"))
            {
                return new ValidationResult("Email must contain '@'.");
            }

            if (email.Count(c => c == '.') < 2)
            {
                return new ValidationResult("Email must contain at least two '.' characters.");
            }
            var instance = validationContext.ObjectInstance;

            var firstNameProperty = validationContext.ObjectType.GetProperty("FirstName");
            var lastNameProperty = validationContext.ObjectType.GetProperty("LastName");

            var firstName = firstNameProperty?.GetValue(instance)?.ToString()?.ToLower();
            var lastName = lastNameProperty?.GetValue(instance)?.ToString()?.ToLower();

            if (!string.IsNullOrWhiteSpace(firstName) && !email.Contains(firstName, StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("Email must contain the first name.");
            }

            if (!string.IsNullOrWhiteSpace(lastName) && !email.Contains(lastName, StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("Email must contain the last name.");
            }

            return ValidationResult.Success;
        }
    }
}
