using MVC.Facade;
using System.ComponentModel.DataAnnotations;
using MVC.Data;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace MVC.Tests.Facade;

[TestClass] public class DiagnosisViewTests : SealedTests<DiagnosisView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Diagnosis");
    [TestMethod] public void MedicineNameTest() => isProperty<string?>("Medicine");
    [TestMethod] public void DescriptionTest() => isProperty<string?>(null);
    [TestMethod] public void RequiresSurgeryTest() => isProperty<bool>("Requires Surgery");
    [TestMethod] public void RequiresPrescriptionTest() => isProperty<bool>("Requires Prescription");


    protected override Type setType() => typeof(DiagnosisView);
    private DiagnosisView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new DiagnosisView
        {
            Id = 1,
            MedicineName = "Valid Medicine",
            Description = "Valid Description",
            RequiresSurgery = true
        };
    }
    [TestMethod]  public void MedicineNameIsRequiredTest()
    {
        view!.MedicineName = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Medicine Name field is required.")));
    }
    [TestMethod] public void MedicineNameLengthTest()
    {
        view!.MedicineName = new string('B', 501);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Medicine cannot exceed 500 characters.")));
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
    [TestMethod] public void RequiresSurgeryTest_2()
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
