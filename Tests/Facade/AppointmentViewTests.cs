using MVC.Facade;
using System.ComponentModel.DataAnnotations;

namespace MVC.Tests.Facade;

[TestClass] public class AppointmentViewTests : SealedTests<AppointmentView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Appointments");
    [TestMethod] public void DateTest() => isProperty<DateTime?>("Date",DataType.Date);
    [TestMethod] public void DoctorIdTest() => isProperty<int>("Doctor");
    [TestMethod] public void PatientIdTest() => isProperty<int>("Patient");
    [TestMethod] public void DoctorFullNameTest() => isProperty<string?>("Doctor");
    [TestMethod] public void PatientFullNameTest() => isProperty<string?>("Patient");
    [TestMethod] public void RoomTest() => isProperty<string?>(null, EntityView.roomEx);
    [TestMethod] public void AppointmentFeeTest() => isProperty<double?>("Appointment Fee");
    protected override Type setType() => typeof(AppointmentView);
    private AppointmentView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new AppointmentView
        {
            Id = 1,
            DoctorId = 2,
            PatientId = 3,
            Date = DateTime.Today,
            Room = "Valid Location",
            AppointmentFee = 100.0
        };
    }
    [TestMethod] public void DateIsRequiredTest()
    {
        view!.Date = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("This field is required.")));
    }
    [TestMethod] public void LocationValidationTest()
    {
        view!.Room = "invalid location";
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("A room must start with two capital letters followed by three digits (e.g., AB302).")));
    }
    [TestMethod] public void AppointmentFeeRangeTest()
    {
        view!.AppointmentFee = -1;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("Appointment Fee must be zero or positive.")));
    }
    private List<ValidationResult> validate(object model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}
