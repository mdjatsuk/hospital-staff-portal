using Microsoft.EntityFrameworkCore;
using MVC.Soft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Tests.Soft.Data;

[TestClass] public class ApplicationDbContextTests :
    BaseClassTests<ApplicationDbContext, IdentityDbContext>
{
    protected override ApplicationDbContext createObj()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        return new ApplicationDbContext(options);
    }
    [TestMethod] public void AppointmentsTest() => isType(obj!.Appointments, typeof(DbSet<AppointmentData>));
    [TestMethod] public void DoctorsTest() => isType(obj!.Doctors, typeof(DbSet<DoctorData>));
    [TestMethod] public void MedicalRecordsTest() => isType(obj!.MedicalRecords, typeof(DbSet<MedicalRecordData>));
    [TestMethod] public void MedicinesTest() => isType(obj!.Medicines, typeof(DbSet<MedicineData>));
    [TestMethod] public void PatientsTest() => isType(obj!.Patients, typeof(DbSet<PatientData>));
}
