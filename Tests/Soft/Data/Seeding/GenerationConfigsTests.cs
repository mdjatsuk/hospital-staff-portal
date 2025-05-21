using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MVC.Aids.Attributes;
using MVC.Data;
using MVC.Soft.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MVC.Tests.Soft.Data.Seeding;

[TestClass] public class GenerationConfigsTests : BaseTests
{
    protected override Type setType() => typeof(GenerationConfigs);
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

        notNull(result);
        equal(1, result.Count);

        additionalAssertions?.Invoke(result[0]);
    }
    [TestMethod] public async Task PatientData_OpenAi()
    {
        await TestOpenAiGenerator<PatientData>();
    }
    [TestMethod] public async Task DoctorData_OpenAi()
    {
        await TestOpenAiGenerator<DoctorData>(item =>
        {
            isFalse(string.IsNullOrWhiteSpace(item["FirstName"]?.ToString()));
            isFalse(string.IsNullOrWhiteSpace(item["LastName"]?.ToString()));
            isFalse(string.IsNullOrWhiteSpace(item["EmailAddress"]?.ToString()));
        });
    }
    [TestMethod] public async Task DiagnosisData_OpenAi()
    {
        await TestOpenAiGenerator<DiagnosisData>(item =>
        {
            isFalse(string.IsNullOrWhiteSpace(item["Diagnosis"]?.ToString()));
            isFalse(string.IsNullOrWhiteSpace(item["Description"]?.ToString()));
            isFalse(string.IsNullOrWhiteSpace(item["Medicine"]?.ToString()));
        });
    }
    [TestMethod] public async Task AppointmentData_OpenAi()
    {
        await TestOpenAiGenerator<AppointmentData>(item =>
        {
            var room = item["Room"]?.ToString();
            isFalse(string.IsNullOrWhiteSpace(room));
            isTrue(System.Text.RegularExpressions.Regex.IsMatch(room, @"^[A-Z]{2}\d{3}$"));
        });
    }
    [TestMethod] public void GetTest()
    {
        PatientData_OpenAi();
        DoctorData_OpenAi();
        DiagnosisData_OpenAi();
        AppointmentData_OpenAi();
    }
}
