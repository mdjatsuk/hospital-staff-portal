using Microsoft.Extensions.Configuration;
using MVC.Data;

namespace MVC.Tests.Soft.OpenAiTests;

[TestClass] public class ParsingTests
{
    private DummyOpenAiService _service;
    private class DummyOpenAiService : OpenAiService
    {
        public DummyOpenAiService() : base(
            new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> { { "OpenAI:ApiKey", "dummy" } })
                .Build()){}

        public new List<(string, string, string)> ParseDiagnosisResponse(string response) => base.ParseDiagnosisResponse(response);
        public new List<(string, T)> ParseResponseWithEnum<T>(string response, Func<string, T?> parseSecondValue) where T : struct
            => base.ParseResponseWithEnum(response, parseSecondValue);
        public new List<string> ParseResponseSeparatedWithComma(string response) => base.ParseResponseSeparatedWithComma(response);
    }
    [TestInitialize] public void Setup()
    {
        _service = new DummyOpenAiService();
    }

    [TestMethod] public void ParseDiagnosisResponseTest()
    {
        string response = "Flu, Fever and cough, Paracetamol;Cold, Runny nose, Ibuprofen;";
        var result = _service.ParseDiagnosisResponse(response);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(("Flu", "Fever and cough", "Paracetamol"), result[0]);
        Assert.AreEqual(("Cold", "Runny nose", "Ibuprofen"), result[1]);
    }

    [TestMethod] public void ParseResponseWithEnumTest()
    {
        string response = "John Doe, 1;Jane Smith, 2;";
        var result = _service.ParseResponseWithEnum<Genders>(response, s =>
        {
            if (int.TryParse(s, out int i) && Enum.IsDefined(typeof(Genders), i))
                return (Genders)i;
            return null;
        });
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(("John Doe", Genders.Male), result[0]);
        Assert.AreEqual(("Jane Smith", Genders.Female), result[1]);
    }

    [TestMethod] public void ParseResponseSeparatedWithCommaTest()
    {
        string response = "AB123;CD456;EF789;";
        var result = _service.ParseResponseSeparatedWithComma(response);
        CollectionAssert.AreEqual(new List<string> { "AB123", "CD456", "EF789" }, result);
    }
}
