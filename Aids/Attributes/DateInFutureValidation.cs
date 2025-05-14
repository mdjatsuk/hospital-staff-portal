using System.ComponentModel.DataAnnotations;

namespace MVC.Aids.Attributes;

public class DateInFutureValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime date) return true;
        return date.Date >= DateTime.Today;
    }

    public override string FormatErrorMessage(string name)
        => $"{name} must be today or in the future.";
}
