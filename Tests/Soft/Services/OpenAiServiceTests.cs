using Microsoft.Extensions.Configuration;
using MVC.Core;
using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Services;

[TestClass] public class OpenAiServiceTests : BaseTests
{
    protected override Type setType() => typeof(OpenAiService);
    private OpenAiService _service;
    [TestInitialize] public void Setup()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        _service = new OpenAiService(config);
    }
    [TestMethod] public void ParseDiagnosisResponseTest()
    {
        string response = "Flu, Fever and cough, Paracetamol;Cold, Runny nose, Ibuprofen;";
        var result = _service.ParseDiagnosisResponse(response);
        equal(2, result.Count);
        equal(("Flu", "Fever and cough", "Paracetamol"), result[0]);
        equal(("Cold", "Runny nose", "Ibuprofen"), result[1]);
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
        equal(2, result.Count);
        equal(("John Doe", Genders.Male), result[0]);
        equal(("Jane Smith", Genders.Female), result[1]);
    }
    [TestMethod] public void ParseResponseSeparatedWithCommaTest()
    {
        string response = "AB123;CD456;EF789;";
        var result = _service.ParseResponseSeparatedWithComma(response);
        CollectionAssert.AreEqual(new List<string> { "AB123", "CD456", "EF789" }, result);
    }
    [TestMethod] public async Task GenerateDataAsyncTest()
    {
        int toGenerate = 2;
        string instruction = "Generate two random room codes (e.g., AB123)";
        Func<string, List<string>> parseResponse = response =>
            response.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        var result = await _service.GenerateDataAsync<string>(toGenerate, instruction, parseResponse);
        notNull(result);
        equal(2, result.Count);
        isTrue(result.All(code => !string.IsNullOrWhiteSpace(code)));
    }
    [DataRow(0)]
    [DataRow(-1)]
    [TestMethod] public async Task GenerateDataAsyncTest1(int toGenerate)
    {
        var result = await _service.GenerateDataAsync<string>(toGenerate, "Instruction", r => new List<string>());
        notNull(result);
        equal(0, result.Count);
    }
    [TestMethod] public async Task GenerateRandomNamesAndGendersAsyncTest()
    {
        int toGenerate = 2;
        var result = await _service.GenerateRandomNamesAndGendersAsync(toGenerate);
        notNull(result);
        equal(toGenerate, result.Count);
        foreach (var (fullName, gender) in result)
        {
            isFalse(string.IsNullOrWhiteSpace(fullName));
            isTrue(gender == Genders.Male || gender == Genders.Female);
        }
    }
    [TestMethod] public async Task GenerateRandomMedicinesAndDescriptionsAsyncTest()
    {
        int toGenerate = 2;
        var result = await _service.GenerateRandomMedicinesAndDescriptionsAsync(toGenerate);
        notNull(result);
        equal(toGenerate, result.Count);
        foreach (var (diagnosis, description, medicine) in result)
        {
            isFalse(string.IsNullOrWhiteSpace(diagnosis));
            isFalse(string.IsNullOrWhiteSpace(description));
            isFalse(string.IsNullOrWhiteSpace(medicine));
            isFalse(description.EndsWith("."));
        }
    }
    [TestMethod] public async Task GenerateRandomRoomsAsyncTest()
    {
        int toGenerate = 2;
        var result = await _service.GenerateRandomRoomsAsync(toGenerate);
        notNull(result);
        equal(toGenerate, result.Count);
        foreach (var room in result)
        {
            isFalse(string.IsNullOrWhiteSpace(room));
            isTrue(System.Text.RegularExpressions.Regex.IsMatch(room, @"^[A-Z]{2}\d{3}$"));
        }
    }
}
