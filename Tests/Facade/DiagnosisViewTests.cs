using MVC.Facade;
using System.ComponentModel.DataAnnotations;
using MVC.Data;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace MVC.Tests.Facade;

[TestClass] public class DiagnosisViewTests : SealedTests<DiagnosisView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Medicines");
    [TestMethod] public void MedicineNameTest() => isProperty<string?>("Medicines");
    [TestMethod] public void DescriptionTest() => isProperty<string?>(null);
    [TestMethod] public void RequiresPrescriptionTest() => isProperty<bool>("Requires Prescription");


    protected override Type setType() => typeof(DiagnosisView);
    private DiagnosisView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new DiagnosisView
        {
            Id = 1,
            Medicine = "Valid Diagnosis",
            Description = "Valid Description",
        };
    }
    [TestMethod]  public void MedicineNameIsRequiredTest()
    {
        view!.Medicine = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Diagnosis Name field is required.")));
    }
    [TestMethod] public void MedicineNameLengthTest()
    {
        view!.Medicine = new string('B', 501);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Diagnosis cannot exceed 500 characters.")));
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
    private List<ValidationResult> validate(object model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}
