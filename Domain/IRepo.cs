using MVC.Core;

namespace MVC.Domain;

public interface IAppointmentsRepo : IRepo<Appointment>;
public interface IDiagnosesRepo : IRepo<Diagnosis>;
public interface IDoctorsRepo : IRepo<Doctor>;
public interface IPatientsRepo : IRepo<Patient>;
public interface IMedicalRecords : IRepo<MedicalRecord> { }
