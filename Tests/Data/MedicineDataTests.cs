using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class MedicineDataTests : SealedTests<MedicineData, EntityData<MedicineData>>
{
    [TestMethod] public void MedicineNameTest() => isProperty<string>();
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
