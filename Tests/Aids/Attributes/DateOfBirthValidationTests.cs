using MVC.Aids.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Aids.Attributes;

[TestClass] public class DateOfBirthValidationTests : BaseTests
{
    protected override Type setType() => typeof(DateOfBirthValidation);
    private readonly DateOfBirthValidation v = new();
    [TestMethod] public void ValidToday()
    {
        var ctx = new ValidationContext(new object());
        var result = v.GetValidationResult(DateTime.Today, ctx);
        equal(ValidationResult.Success, result);
    }
    [TestMethod] public void ValidPast()
    {
        var ctx = new ValidationContext(new object());
        var result = v.GetValidationResult(DateTime.Today.AddYears(-30), ctx);
        equal(ValidationResult.Success, result);
    }
    [TestMethod] public void InvalidFuture()
    {
        var ctx = new ValidationContext(new object());
        var result = v.GetValidationResult(DateTime.Today.AddDays(1), ctx);
        notNull(result);
        equal("Date of Birth cannot be in the future.", result?.ErrorMessage);
    }
    [TestMethod] public void IsValidTest()
    {
        ValidToday();
        ValidPast();
        InvalidFuture();
    }
}

