using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class DiagnosisDataTests : ClassTests<DiagnosisData, EntityData<DiagnosisData>>
{
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.Description = "Test Description";
        obj.RequiresSurgery = true;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal("Test Description", d?.Description);
        equal(true, d?.RequiresSurgery);
    }
}
