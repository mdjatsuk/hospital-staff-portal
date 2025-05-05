using MVC.Facade;
using System.ComponentModel.DataAnnotations;
using MVC.Data;

namespace MVC.Tests.Facade;

[TestClass] public class DiagnosisViewTests : SealedTests<DiagnosisView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Diagnosis");
    [TestMethod] public void DiagnosisNameTest() => isProperty<Diagnoses?>("Diagnosis Name");
    [TestMethod] public void DescriptionTest() => isProperty<string?>(null);
    [TestMethod] public void RequiresSurgeryTest() => isProperty<bool>("Requires Surgery");
    protected override Type setType() => typeof(DiagnosisView);
    private DiagnosisView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new DiagnosisView
        {
            Id = 1,
            DiagnosisName = Diagnoses.Anemia,
            Description = "Valid Description",
            RequiresSurgery = true
        };
    }
    [TestMethod]  public void DiagnosisNameIsRequiredTest()
    {
        view!.DiagnosisName = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Diagnosis Name field is required.")));
    }
    [TestMethod] public void DiagnosisNameLengthTest()
    {
        view!.DiagnosisName = Diagnoses.Anemia;
        var diagnosisNameString = view!.DiagnosisName.ToString();
        var results = validate(view);
        isTrue(diagnosisNameString.Length <= 500, "Diagnosis Name length exceeds the limit.");
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
