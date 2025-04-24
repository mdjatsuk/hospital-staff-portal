using MVC.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class DiagnosisViewTests : BaseTests
{
    private DiagnosisView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new DiagnosisView
        {
            Id = 1,
            DiagnosisName = "Valid Diagnosis",
            Description = "Valid Description",
            RequiresSurgery = true
        };
    }
    [TestMethod] public void DiagnosisNameIsRequiredTest()
    {
        view!.DiagnosisName = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Diagnosis Name field is required.")));
    }
    [TestMethod] public void DiagnosisNameLengthTest()
    {
        view!.DiagnosisName = new string('A', 101);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Diagnosis Name cannot exceed 100 characters.")));
    }
    [TestMethod] public void DescriptionIsRequiredTest()
    {
        view!.Description = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Description field is required.")));
    }
    [TestMethod] public void DescriptionLengthTest()
    {
        view!.Description = new string('A', 501);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Description cannot exceed 500 characters.")));
    }
    [TestMethod] public void RequiresSurgeryTest()
    {
        view!.RequiresSurgery = false;
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
