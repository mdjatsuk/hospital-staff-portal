using MVC.Data;

namespace MVC.Tests.Data;

[TestClass] public class DiagnosisDataTests : SealedTests<DiagnosisData, EntityData<DiagnosisData>>
{
    [TestMethod] public void RecordNrTest() => isProperty<string>();
    [TestMethod] public void DiagnosisTest() => isProperty<string>();
    [TestMethod] public void MedicineTest() => isProperty<string>();
    [TestMethod] public void DescriptionTest() => isProperty<string>();
    [TestMethod] public void RequiresPrescriptionTest() => isProperty<bool>();
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.Medicine = "Ibuprofen";
        obj.Description = "Test Description";
        obj.RequiresPrescription = true;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal("Ibuprofen", d?.Medicine);
        equal("Test Description", d?.Description);
        equal(true, d?.RequiresPrescription);
    }
}
