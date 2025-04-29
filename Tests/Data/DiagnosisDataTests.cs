using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class DiagnosisDataTests : SealedTests<DiagnosisData, EntityData<DiagnosisData>>
{
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.DiagnosisName = DiagnosisEnum.Anemia;
        obj.Description = "Test Description";
        obj.RequiresSurgery = true;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal(DiagnosisEnum.Anemia, d?.DiagnosisName);
        equal("Test Description", d?.Description);
        equal(true, d?.RequiresSurgery);
    }
}
