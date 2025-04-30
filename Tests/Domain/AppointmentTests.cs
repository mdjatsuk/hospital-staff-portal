using MVC.Data;
using MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Domain;

[TestClass] public class AppointmentTests : BaseClassTests<Appointment, Entity<AppointmentData>>
{
    protected override Appointment createObj()
    {
        var d = new AppointmentData
        {
            Id = 1,
            DoctorId = 2,
            PatientId = 3,
            Date = DateTime.Today,
            Room = "Test Location",
            AppointmentFee = 100.0
        };
        return new Appointment(d);
    }
    [TestMethod] public void DoctorIdTest() => equal(2, obj?.DoctorId);
    [TestMethod] public void PatientIdTest() => equal(3, obj?.PatientId);
    [TestMethod] public void DateTest() => equal(DateTime.Today, obj?.Date);
    [TestMethod] public void LocationTest() => equal("Test Location", obj?.Location);
    [TestMethod] public void AppointmentFeeTest() => equal(100.0, obj?.AppointmentFee);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
}
