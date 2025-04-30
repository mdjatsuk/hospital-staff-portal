using MVC.Aids.Attributes;
using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class PatientViewTests : SealedTests<PatientView, EntityView>
{
    [TestMethod] public void FirstNameTest() => isProperty<string?>("First Name");
    [TestMethod] public void LastNameTest() => isProperty<string?>("Last Name");
    [TestMethod] public void DateOfBirthTest() => isProperty<DateTime?>("Date Of Birth");
    [TestMethod] public void GenderTest() => isProperty<Genders?>(null);
    protected override Type setType() => typeof(PatientView);
    private PatientView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new PatientView
        {
            Id = 1,
            FirstName = "Valid First Name",
            LastName = "Valid Last Name",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Genders.Female,
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
    [TestMethod] public void DateOfBirthIsRequiredTest()
    {
        view!.DateOfBirth = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Date Of Birth field is required.")));
    }
    [TestMethod] public void GenderValidationTest()
    {
        view!.Gender = null;
        var results = validate(view);
        isFalse(results.Any(r => r.ErrorMessage!.Contains("The field Gender is required.")));
    }
    [TestMethod] public void DiagnosisIdValidationTest()
    {
        var results = validate(view);
        isFalse(results.Any());
    }
    private List<ValidationResult> validate(object model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}