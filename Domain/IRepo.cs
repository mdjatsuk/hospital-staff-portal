using MVC.Core;

namespace MVC.Domain;

public interface IAppointmentsRepo : IRepo<Appointment>;
public interface IMedicinesRepo : IRepo<Medicine>;
public interface IDoctorsRepo : IRepo<Doctor>;
public interface IPatientsRepo : IRepo<Patient>;
public interface IMedicalRecordsRepo : IRepo<MedicalRecord> { }
