using MVC.Aids;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Tests.Facade;

[TestClass] public class AppointmentViewTests : SealedTests<AppointmentView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Appointments");
    [TestMethod] public void DateTest() => isProperty<DateTime?>("Date",DataType.Date);
    [TestMethod] public void DoctorIdTest() => isProperty<int>("Doctor");
    [TestMethod] public void PatientIdTest() => isProperty<int>("Patient");
    [TestMethod] public void DoctorTest() => isProperty<string?>("Doctor");
    [TestMethod] public void PatientTest() => isProperty<string?>("Patient");
    [TestMethod] public void LocationTest() => isProperty<string?>(null, EntityView.locationEx);
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
            Location = "Valid Location",
            AppointmentFee = 100.0
        };
    }
    [TestMethod] public void DateIsRequiredTest()
    {
        view!.Date = null;
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("The Date field is required.")));
    }
    [TestMethod] public void LocationValidationTest()
    {
        view!.Location = "invalid location";
        var results = validate(view);
        isTrue(results.Any(r => r.ErrorMessage!.Contains("Location must start with a capital letter.")));
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
