using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVC.Core.Editors;
using MVC.Data;
using MVC.Soft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Data;

[TestClass]
public class DbInitializerTests : BaseTests
{
    protected override Type setType() => typeof(DbInitializer);
    private ApplicationDbContext _context;
    private OpenAiService _openAi;
    private DbInitializer _initializer;
    [TestInitialize] public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
            { "OpenAi:ApiKey", "REDACTED_OPENAI_KEY" }
            })
            .Build();
        _openAi = new OpenAiService(config);
        _initializer = new DbInitializer(_context, _openAi);
    }
    [TestMethod] public async Task InitializeTest()
    {
        await _initializer.Initialize(2);
        isTrue(await _context.Patients.CountAsync() >= 2, "Patients not seeded.");
        isTrue(await _context.Doctors.CountAsync() >= 2, "Doctors not seeded.");
        isTrue(await _context.Diagnoses.CountAsync() >= 2, "Diagnoses not seeded.");
        isTrue(await _context.Appointments.CountAsync() >= 2, "Appointments not seeded.");
        isTrue(await _context.MedicalRecords.CountAsync() >= 2, "MedicalRecords not seeded.");
    }
    [TestMethod] public async Task SeedTest()
    {
        int initialCount = await _context.Patients.CountAsync();
        var method = typeof(DbInitializer).GetMethod("Seed", BindingFlags.NonPublic | BindingFlags.Instance);
        var task = (Task)method.MakeGenericMethod(typeof(PatientData)).Invoke(_initializer, new object[] { initialCount + 3 });
        await task;
        int afterCount = await _context.Patients.CountAsync();
        equal(initialCount + 3, afterCount, "Seed should add the correct number of PatientData entities.");
    }
    [TestMethod] public async Task GetReferenceValuesTest()
    {
        var doctor = new DoctorData { FirstName = "Doc", LastName = "Tor" };
        var patient = new PatientData { FirstName = "Pat", LastName = "Ient" };
        _context.Doctors.Add(doctor);
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        var method = typeof(DbInitializer).GetMethod("GetReferenceValues", BindingFlags.NonPublic | BindingFlags.Instance);
        var task = (Task<Dictionary<string, object>>)method.MakeGenericMethod(typeof(AppointmentData)).Invoke(_initializer, null);
        var result = await task;
        notNull(result);
        isTrue(result.ContainsKey("DoctorId"), "DoctorId not found in reference values.");
        isTrue(result.ContainsKey("PatientId"), "PatientId not found in reference values.");
        equal(doctor.Id, result["DoctorId"]);
        equal(patient.Id, result["PatientId"]);
        equal("Doc Tor", result["DoctorFullName"]);
        equal("Pat Ient", result["PatientFullName"]);
    }
}
