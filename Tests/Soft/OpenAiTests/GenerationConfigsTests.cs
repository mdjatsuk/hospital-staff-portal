using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MVC.Data;
using MVC.Soft.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MVC.Tests.Soft.OpenAiTests;

[TestClass] public class GenerationConfigsTests
{
    private static IConfigurationRoot configuration;
    [ClassInitialize] public static void Init(TestContext context)
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }
    private async Task TestOpenAiGenerator<T>(Action<Dictionary<string, object>>? additionalAssertions = null)
    {
        var openAiService = new OpenAiService(configuration);
        var config = GenerationConfigs.Get<T>(openAiService);
        var result = await config.OpenAiGenerator!(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);

        additionalAssertions?.Invoke(result[0]);
    }
    [TestMethod] public async Task PatientData_OpenAiGenerator()
    {
        await TestOpenAiGenerator<PatientData>();
    }
    [TestMethod] public async Task DoctorData_OpenAiGenerator()
    {
        await TestOpenAiGenerator<DoctorData>(item =>
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(item["FirstName"]?.ToString()));
            Assert.IsFalse(string.IsNullOrWhiteSpace(item["LastName"]?.ToString()));
            Assert.IsFalse(string.IsNullOrWhiteSpace(item["EmailAddress"]?.ToString()));
        });
    }
    [TestMethod] public async Task DiagnosisData_OpenAiGenerator()
    {
        await TestOpenAiGenerator<DiagnosisData>(item =>
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(item["Diagnosis"]?.ToString()));
            Assert.IsFalse(string.IsNullOrWhiteSpace(item["Description"]?.ToString()));
            Assert.IsFalse(string.IsNullOrWhiteSpace(item["Medicine"]?.ToString()));
        });
    }
    [TestMethod] public async Task AppointmentData_OpenAiGenerator()
    {
        await TestOpenAiGenerator<AppointmentData>(item =>
        {
            var room = item["Room"]?.ToString();
            Assert.IsFalse(string.IsNullOrWhiteSpace(room));
            Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(room, @"^[A-Z]{2}\d{3}$"));
        });
    }
}
