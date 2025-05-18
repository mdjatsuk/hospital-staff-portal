using MVC.Data;
using MVC.Domain;

namespace MVC.Tests.Domain;

[TestClass] public class DiagnosisTests : SealedTests<Diagnosis, Entity<DiagnosisData>>
{
    protected override Diagnosis createObj()
    {
        var d = new DiagnosisData
        {
            Id = 1,
            RecordNr = "RN123",
            Diagnosis = "Flu",
            Medicine = "Paracetamol",
            Description = "Test Description",
        };
        return new Diagnosis(d);
    }
    [TestMethod] public void MedicineNameTest() => equal("Paracetamol", obj?.MedicineName);
    [TestMethod] public void DescriptionTest() => equal("Test Description", obj?.Description);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
    [TestMethod] public void RecordNrTest() => equal("RN123", obj?.RecordNr);
    [TestMethod] public void DiagnosisNameTest() => equal("Flu", obj?.DiagnosisName);
    [TestMethod] public void RequiresPrescriptionTest()
    {
        var data = new DiagnosisData { RequiresPrescription = true };
        var diagnosis = new Diagnosis(data);
        Assert.IsTrue(diagnosis.RequiresPrescription);
        data = new DiagnosisData { RequiresPrescription = false };
        diagnosis = new Diagnosis(data);
        Assert.IsFalse(diagnosis.RequiresPrescription);
    }
}