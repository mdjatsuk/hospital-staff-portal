using MVC.Facade;
using System.ComponentModel.DataAnnotations;

namespace MVC.Tests.Facade;

[TestClass] public class DiagnosisViewTests : SealedTests<DiagnosisView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Diagnoses");
    [TestMethod] public void MedicineTest() => isProperty<string?>("Medicines");
    [TestMethod] public void DescriptionTest() => isProperty<string?>(null);
    [TestMethod] public void RequiresPrescriptionTest() => isProperty<bool>("Requires Prescription");
    [TestMethod] public void RecordNrTest() => isProperty<string?>("Record Number", @"^#(9999|[1-9][0-9]{2,3})$");
    [TestMethod] public void DiagnosisTest() => isProperty<string?>("Diagnosis", @"^[A-Z].*$");
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
    [TestMethod] public void MedicineNameIsRequiredTest()
    {
        view!.Medicine = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("This field is required.")));
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
        isTrue(results.Any(r => r.ErrorMessage!.Contains("This field is required.")));
    }
    [TestMethod] public void DescriptionLengthTest()
    {
        view!.Description = new string('A', 501);
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Description cannot exceed 500 characters.")));
    }
    [TestMethod] public void RecordNrIsRequiredTest()
    {
        view!.RecordNr = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("This field is required.")));
    }
    [TestMethod] public void DiagnosisIsRequiredTest()
    {
        view!.Diagnosis = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("This field is required.")));
    }
    private List<ValidationResult> validate(object model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}
