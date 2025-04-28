using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class PatientDataTests : ClassTests<PatientData, EntityData<PatientData>>
{
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.FirstName = "Jane";
        obj.LastName = "Doe";
        obj.DateOfBirth = new DateTime(1990, 1, 1);
        obj.Gender = Genders.Female;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal("Jane", d?.FirstName);
        equal("Doe", d?.LastName);
        equal(new DateTime(1990, 1, 1), d?.DateOfBirth);
        equal(Genders.Female, d?.Gender);
    }
}
