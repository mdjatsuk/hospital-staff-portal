using MVC.Core;
using MVC.Data;
using MVC.Domain;
using Random = MVC.Aids.Random;

namespace MVC.Tests.Domain;

[TestClass] public class AppointmentTests : SealedTests<Appointment, Entity<AppointmentData>>
{
    AppointmentData? data = null;

    protected override Appointment createObj()
    {
        data = Random.Object<AppointmentData>();
        return new Appointment(data);
    }

    [TestMethod] public void DoctorIdTest() => isReadOnly(data!.DoctorId);
    [TestMethod] public void PatientIdTest() => isReadOnly(data!.PatientId);
    [TestMethod] public void DateTest() => isReadOnly(data!.Date);
    [TestMethod] public void LocationTest() => isReadOnly(data!.Room);
    [TestMethod] public void AppointmentFeeTest() => isReadOnly<double?>(data!.AppointmentFee);
    [TestMethod] public void DoctorTest() => isReadOnly<Doctor>(null);
    [TestMethod]
    public void PatientTest()
    {
        var patientId = 42;
        var appointmentData = new AppointmentData { PatientId = patientId };
        var appointment = new Appointment(appointmentData);
        Assert.AreEqual(patientId, appointment.PatientId);
    }

    [TestMethod]
    public void DoctorFullNameTest()
    {
        var expected = "Jane Smith";
        var appointmentData = new AppointmentData { DoctorFullName = expected };
        var appointment = new Appointment(appointmentData);
        Assert.AreEqual(expected, appointment.data?.DoctorFullName);
    }

    [TestMethod]
    public void PatientFullNameTest()
    {
        var expected = "John Doe";
        var appointmentData = new AppointmentData { PatientFullName = expected };
        var appointment = new Appointment(appointmentData);
        Assert.AreEqual(expected, appointment.data?.PatientFullName);
    }
    [TestMethod] public async Task LoadLazyTest()
    {
        var repo = new mockDoctorRepo();
        var d = Random.Object<DoctorData>();
        d.Id = data!.DoctorId;
        var o = new Doctor(d);
        repo.list.Add(o);   
        for(var i = 0; i < Random.UInt8(5,10); i++)
            repo.list.Add(new Doctor(Random.Object<DoctorData>()));
        Services.services.Add(typeof(IDoctorsRepo), repo);
        await obj!.LoadLazy();
        equal(o?.Id, d?.Id);
    }
}
