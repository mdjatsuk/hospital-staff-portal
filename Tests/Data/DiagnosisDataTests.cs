using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class DiagnosisDataTests : SealedTests<DiagnosisData, EntityData<DiagnosisData>>
{
    [TestMethod] public void DiagnosisNameTest() => isProperty<Diagnoses?>();
    [TestMethod] public void DescriptionTest() => isProperty<string>();
    [TestMethod] public void RequiresSurgeryTest() => isProperty<bool>();
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.DiagnosisName = Diagnoses.Anemia;
        obj.Description = "Test Description";
        obj.RequiresSurgery = true;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal(Diagnoses.Anemia, d?.DiagnosisName);
        equal("Test Description", d?.Description);
        equal(true, d?.RequiresSurgery);
    }
}
