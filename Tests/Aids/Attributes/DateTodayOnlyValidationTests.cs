using MVC.Aids.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Aids.Attributes;

[TestClass] public class DateTodayOnlyValidationTests : BaseTests
{
    protected override Type setType() => typeof(DateTodayOnlyValidation);
    private readonly DateTodayOnlyValidation v = new();
    [TestMethod] public void ValidWhenDateIsToday() => isTrue(v.IsValid(DateTime.Today));
    [TestMethod] public void InvalidWhenDateIsInPast() => isFalse(v.IsValid(DateTime.Today.AddDays(-1)));
    [TestMethod] public void InvalidWhenDateIsInFuture() => isFalse(v.IsValid(DateTime.Today.AddDays(1)));
    [TestMethod] public void ValidWhenNotDateTime() => isTrue(v.IsValid(123));
    [TestMethod] public void ErrorMessageIsCorrect()
    {
        var message = v.FormatErrorMessage("EventDate");
        equal("EventDate must be today date.", message);
    }
    [TestMethod] public void IsValidTest()
    {
        ValidWhenDateIsToday();
        InvalidWhenDateIsInPast();
        InvalidWhenDateIsInFuture();
        ValidWhenNotDateTime();
    }
    [TestMethod] public void FormatErrorMessageTest() => ErrorMessageIsCorrect();
}