using Microsoft.Extensions.Configuration;

namespace MVC.Tests.Soft.OpenAiTests;

[TestClass] public class GenerateDataAsyncTests
{
    private OpenAiService _service;
    [TestInitialize] public void Setup()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        _service = new OpenAiService(config);
    }
    [TestMethod] public async Task GenerateDataAsync_IntegrationTest()
    {
        int toGenerate = 2;
        string instruction = "Generate two random room codes (e.g., AB123)";
        Func<string, List<string>> parseResponse = response =>
            response.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        var result = await _service.GenerateDataAsync<string>(toGenerate, instruction, parseResponse);
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(code => !string.IsNullOrWhiteSpace(code)));
    }
    [DataRow(0)]
    [DataRow(-1)]
    [TestMethod] public async Task GenerateDataAsync_InvalidToGenerate_ReturnsEmptyList(int toGenerate)
    {
        var result = await _service.GenerateDataAsync<string>(toGenerate, "Instruction", r => new List<string>());
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }
}
