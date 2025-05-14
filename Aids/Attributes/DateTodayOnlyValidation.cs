using System.ComponentModel.DataAnnotations;

namespace MVC.Aids.Attributes;

public class DateTodayOnlyValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime date) return true;
        var today = DateTime.Today;
        return date.Date == today;
    }

    public override string FormatErrorMessage(string name)
        => $"{name} must be today date.";
}
