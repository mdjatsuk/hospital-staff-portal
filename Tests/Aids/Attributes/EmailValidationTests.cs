using MVC.Aids.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Aids.Attributes;

[TestClass] public class EmailValidationTests : BaseTests
{
    protected override Type setType() => typeof(EmailValidation);
    private class User
    {
        public string FirstName { get; set; } = "John";
        public string LastName { get; set; } = "Doe";
        [EmailValidation] public string Email { get; set; } = "";
    }
    private ValidationContext GetContext(User user)
        => new(user) { MemberName = nameof(User.Email) };
    private ValidationResult? Validate(User user)
    {
        var context = GetContext(user);
        var prop = typeof(User).GetProperty(nameof(User.Email));
        var attr = (EmailValidation?)Attribute.GetCustomAttribute(prop!, typeof(EmailValidation));
        return attr?.GetValidationResult(user.Email, context);
    }
    [TestMethod] public void InvalidIfNullOrEmpty()
    {
        var user = new User { Email = "" };
        equal("Email is required.", Validate(user)?.ErrorMessage);
    }
    [TestMethod] public void InvalidIfMissingAt()
    {
        var user = new User { Email = "john.doeexample.com" };
        equal("Email must contain '@'.", Validate(user)?.ErrorMessage);
    }
    [TestMethod] public void InvalidIfLessThanTwoDots()
    {
        var user = new User { Email = "john@doe.com" };
        equal("Email must contain at least two '.' characters.", Validate(user)?.ErrorMessage);
    }
    [TestMethod] public void InvalidIfMissingFirstName()
    {
        var user = new User { Email = "doe.john@example.co.uk" };
        user.FirstName = "Alice";
        equal("Email must contain the first name.", Validate(user)?.ErrorMessage);
    }
    [TestMethod] public void InvalidIfMissingLastName()
    {
        var user = new User { Email = "john@example.co.uk" };
        user.LastName = "Smith";
        equal("Email must contain the last name.", Validate(user)?.ErrorMessage);
    }
    [TestMethod] public void ValidEmail()
    {
        var user = new User { Email = "john.doe@mail.example.com" };
        var result = Validate(user);
        equal(ValidationResult.Success, result);
    }
}
