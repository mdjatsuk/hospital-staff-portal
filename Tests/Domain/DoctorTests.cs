using MVC.Core;
using MVC.Data;
using MVC.Domain;
using Random = MVC.Aids.Random;

namespace MVC.Tests.Domain;

[TestClass] public class DoctorTests : SealedTests<Doctor, Entity<DoctorData>>
{
    protected override Doctor createObj()
    {
        var d = new DoctorData
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Specialization = Specialities.Cardiology,
            PhoneNumber = 12345678
        };
        return new Doctor(d);
    }
    [TestMethod] public void FirstNameTest() => equal("John", obj?.FirstName);
    [TestMethod] public void LastNameTest() => equal("Doe", obj?.LastName);
    [TestMethod] public void SpecializationTest() => equal(Specialities.Cardiology, obj?.Specialization);
    [TestMethod] public void PhoneNumberTest() => equal(12345678, obj?.PhoneNumber);
    [TestMethod] public void FullNameTest() => equal("John Doe", obj?.FullName);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
    [TestMethod] public void appointmentsTest() => notNull(obj?.appointments);
    [TestMethod] public void EmailAddressTest()
    {
        var data = new DoctorData { EmailAddress = "doc@example.com" };
        var doctor = new Doctor(data);
        equal("doc@example.com", doctor.EmailAddress);
    }
    [TestMethod] public async Task PatientsTest()
    {
        Services.services.Clear();
        var doctorId = 1;
        var patient1 = new Patient(new PatientData { Id = 101, FirstName = "Alice" });
        var patient2 = new Patient(new PatientData { Id = 102, FirstName = "Bob" });
        var appointment1 = new Appointment(new AppointmentData { DoctorId = doctorId, PatientId = 101 });
        var appointment2 = new Appointment(new AppointmentData { DoctorId = doctorId, PatientId = 102 });
        var mockAppointmentRepo = new mockAppointmentRepo();
        mockAppointmentRepo.list.AddRange(new[] { appointment1, appointment2 });
        var mockPatientRepo = new mockPatientRepo();
        mockPatientRepo.list.AddRange(new[] { patient1, patient2 });
        Services.services.Add(typeof(IAppointmentsRepo), mockAppointmentRepo);
        Services.services.Add(typeof(IPatientsRepo), mockPatientRepo);
        var doctor = new Doctor(new DoctorData { Id = doctorId });
        await doctor.LoadLazy();
        equal(2, doctor.Patients.Count);
        isTrue(doctor.Patients.Any(p => p?.Id == 101));
        isTrue(doctor.Patients.Any(p => p?.Id == 102));
    }
    [TestMethod] public async Task LoadLazyTest()
    {
        Services.Clear();
        var doctorId = 1;
        var appointment1 = new Appointment(new AppointmentData { DoctorId = doctorId, PatientId = 101 });
        var appointment2 = new Appointment(new AppointmentData { DoctorId = doctorId, PatientId = 102 });
        var patient1 = new Patient(new PatientData { Id = 101 });
        var patient2 = new Patient(new PatientData { Id = 102 });
        var mockAppointmentRepo = new mockAppointmentRepo();
        mockAppointmentRepo.list.AddRange(new[] { appointment1, appointment2 });
        var mockPatientRepo = new mockPatientRepo();
        mockPatientRepo.list.AddRange(new[] { patient1, patient2 });
        Services.Add(typeof(IAppointmentsRepo), mockAppointmentRepo);
        Services.Add(typeof(IPatientsRepo), mockPatientRepo);
        obj!.data.Id = doctorId;
        await obj.LoadLazy();
        isTrue(obj.appointments.Any(a => a.DoctorId == doctorId && (a.PatientId == 101 || a.PatientId == 102)));
        equal(2, obj.appointments.Count(a => a.DoctorId == doctorId));
    }


}