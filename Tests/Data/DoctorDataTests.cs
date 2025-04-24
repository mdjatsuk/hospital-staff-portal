using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class DoctorDataTests : ClassTests<DoctorData, EntityData<DoctorData>>
{
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.FirstName = "John";
        obj.LastName = "Doe";
        obj.Specialization = Specialties.Cardiology;
        obj.PhoneNumber = "12345678";
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal("John", d?.FirstName);
        equal("Doe", d?.LastName);
        equal(Specialties.Cardiology, d?.Specialization);
        equal("12345678", d?.PhoneNumber);
    }
}
