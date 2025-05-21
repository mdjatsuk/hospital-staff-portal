using MVC.Aids.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Aids.Attributes;

[TestClass] public class DateInFutureValidationTests : BaseTests
{
    protected override Type setType() => typeof(DateInFutureValidation);
    private readonly DateInFutureValidation v = new();
    [TestMethod] public void ValidWhenDateIsToday() => isTrue(v.IsValid(DateTime.Today));
    [TestMethod] public void ValidWhenDateIsInFuture() => isTrue(v.IsValid(DateTime.Today.AddDays(1)));
    [TestMethod] public void InvalidWhenDateIsInPast() => isFalse(v.IsValid(DateTime.Today.AddDays(-1)));
    [TestMethod] public void ValidWhenValueIsNotDateTime() => isTrue(v.IsValid("not a date"));
    [TestMethod] public void ErrorMessageIsCorrect()
    {
        var message = v.FormatErrorMessage("StartDate");
        equal("StartDate must be today or in the future.", message);
    }
    [TestMethod] public void IsValidTest()
    {
        ValidWhenDateIsToday();
        ValidWhenDateIsInFuture();
        InvalidWhenDateIsInPast();
        ValidWhenValueIsNotDateTime();
    }
    [TestMethod] public void FormatErrorMessageTest() => ErrorMessageIsCorrect();
}