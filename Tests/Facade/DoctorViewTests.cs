using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class DoctorViewTests : BaseTests
{
    protected override Type setType() => typeof(DoctorView);
    private DoctorView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new DoctorView
        {
            Id = 1,
            FirstName = "Valid First Name",
            LastName = "Valid Last Name",
            Specialization = Specialties.Cardiology,
            PhoneNumber = 12345678
        };
    }
    [TestMethod] public void FirstNameIsRequiredTest()
    {
        view!.FirstName = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The First Name field is required.")));
    }
    [TestMethod] public void FirstNameLengthTest()
    {
        view!.FirstName = new string('A', 51);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("First Name must be between 2 and 50 characters.")));
    }
    [TestMethod] public void LastNameIsRequiredTest()
    {
        view!.LastName = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Last Name field is required.")));
    }
    [TestMethod] public void LastNameLengthTest()
    {
        view!.LastName = new string('A', 51);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("Last Name must be between 2 and 50 characters.")));
    }
    [TestMethod] public void PhoneNumberValidationTest()
    {
        view!.PhoneNumber = 123;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The phone number must be exactly 8 digits.")));
    }
    [TestMethod] public void SpecializationTest()
    {
        view!.Specialization = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Specialization field is required.")));
    }
    private List<ValidationResult> validate(object model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}