using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class AppointmentDataTests : SealedTests<AppointmentData, EntityData<AppointmentData>>
{
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.DoctorId = 1;
        obj.PatientId = 2;
        obj.Date = DateTime.Today;
        obj.Location = "Test Location";
        obj.AppointmentFee = 100.0;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal(1, d?.DoctorId);
        equal(2, d?.PatientId);
        equal(DateTime.Today, d?.Date);
        equal("Test Location", d?.Location);
        equal(100.0, d?.AppointmentFee);
    }
}
