using MVC.Facade;
using System.ComponentModel.DataAnnotations;

namespace MVC.Tests.Facade;

[TestClass] public class MedicalRecordViewTests : SealedTests<MedicalRecordView, EntityView>
{
    [TestMethod] public override void DisplayNameTest() => isDisplayName("Medical Records");
    [TestMethod] public void PatientIdTest() => isProperty<int>("Patient");
    [TestMethod] public void RecordNrIdTest() => isProperty<int>("Record Nr");
    [TestMethod] public void PatientFullNameTest() => isProperty<string?>("Patient");
    [TestMethod] public void PatientTest() => isProperty<string?>("Patient");
    [TestMethod] public void RecordNrTest() => isProperty<string?>("Record Nr");
    [TestMethod] public void DiagnosedOnTest() => isProperty<DateTime?>("Diagnosed on", DataType.Date);
    protected override Type setType() => typeof(MedicalRecordView);
    private MedicalRecordView? view;
    [TestInitialize] public void TestInitialize()
    {
        view = new MedicalRecordView
        {
            Id = 1,
            PatientId = 2,
            Patient = "Liam Thompson",
            DiagnosedOn = DateTime.Today.AddDays(-1),
            PatientFullName = "Liam Thompson"
        };
    }
}