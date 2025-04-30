using MVC.Data;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;

public class OpenAiService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private const int MaxBatchSize = 1000;

    public OpenAiService(IConfiguration config)
    {
        _http = new HttpClient();
        _apiKey = config["OpenAI:ApiKey"] ?? throw new Exception("OpenAI API key not found.");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<List<T>> GenerateDataAsync<T>(int toGenerate, string instruction, Func<string, List<T>> parseResponse)
    {
        var result = new List<T>();
        int generatedCount = 0;
        int remainingData = toGenerate;

        while (remainingData > 0)
        {
            int requestCount = Math.Min(remainingData, MaxBatchSize);
            var requestPayload = BuildOpenAiRequest(requestCount, instruction);
            var responseString = await SendOpenAiRequestAsync(requestPayload);

            if (string.IsNullOrWhiteSpace(responseString))
                continue;

            var parsed = parseResponse(responseString);
            if (parsed == null || parsed.Count == 0)
                continue;

            foreach (var entry in parsed)
            {
                result.Add(entry);
                generatedCount++;
                if (generatedCount >= toGenerate)
                    break;
            }

            remainingData = toGenerate - generatedCount;

            if (remainingData > 0)
            {
                Console.WriteLine($"Generated {generatedCount}. Still missing {remainingData} entries, retrying...");
            }
        }

        return result.Take(toGenerate).ToList();
    }

    private object BuildOpenAiRequest(int count, string instruction)
    {
        return new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = instruction },
                new { role = "user", content = instruction }
            },
            temperature = 0.7
        };
    }

    private async Task<string?> SendOpenAiRequestAsync(object requestPayload)
    {
        var json = JsonSerializer.Serialize(requestPayload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync("https://api.openai.com/v1/chat/completions", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"OpenAI request failed: {responseBody}");

        try
        {
            using var doc = JsonDocument.Parse(responseBody);
            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString()?
                      .Trim();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected response format: {responseBody}", ex);
        }
    }

    public List<(string, T)> ParseResponse<T>(string response, Func<string, T?> parseSecondValue) where T : struct
    {
        var entries = new List<(string, T)>();
        var uniqueEntries = new HashSet<(string, T)>();
        var rawEntries = response.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawEntry in rawEntries)
        {
            var parts = rawEntry.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                continue;

            var name = parts[0].Trim();
            var secondValueStr = parts[1].Trim();

            var secondValue = parseSecondValue(secondValueStr);

            if (secondValue.HasValue)
            {
                var entry = (name, secondValue.Value);

                if (uniqueEntries.Add(entry))
                {
                    entries.Add(entry);
                }
            }
        }

        return entries;
    }

    private List<string> ParseResponseToList(string response)
    {
        var entries = new List<string>();
        var rawEntries = response.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var entry in rawEntries)
        {
            entries.Add(entry.Trim());
        }

        return entries;
    }

    public async Task<List<(string fullName, Genders gender)>> GenerateRandomNamesAndGendersAsync(int toGenerate)
    {
        string instruction = $"You are an API that generates random full names (first and last) with gender (1 for Male, 2 for Female). " +
                             $"Generate exactly {toGenerate} full names, each followed by a comma and gender. " +
                             $"Separate each entry with a semicolon (;). No extra text. Exact count required.";

        return await GenerateDataAsync(toGenerate, instruction, response => ParseResponse<Genders>(response, genderStr =>
        {
            if (int.TryParse(genderStr, out int genderInt) && Enum.IsDefined(typeof(Genders), genderInt))
            {
                return (Genders)genderInt;
            }
            return null;
        }));
    }


    public async Task<List<(string description, Diagnoses diagnosis)>> GenerateRandomDiagnosisDescriptionsAsync(int toGenerate)
    {
        string instruction = $"You are a medical AI that generates random, **very short** and realistic diagnosis descriptions, each followed by a diagnosis number. " +
                             $"Use the following mapping: Hypertension (1), Diabetes (2), Asthma (3), Epilepsy (4), Pneumonia (5), Tuberculosis (6), " +
                             $"Osteoarthritis (7), Migraine (8), Anemia (9), Gastric Ulcer (10), Hepatitis (11). " +
                             $"Generate exactly {toGenerate} entries. Each entry must follow this format: description,diagnosis_number. " +
                             $"Separate entries using a semicolon (;). Do not add any extra text or explanations. Output only the data.";


        return await GenerateDataAsync(toGenerate, instruction, response => ParseResponse<Diagnoses>(response, diagnosisStr => 
        {
            if (int.TryParse(diagnosisStr, out int diagnosisInt) && Enum.IsDefined(typeof(Diagnoses), diagnosisInt))
            {
                return (Diagnoses)diagnosisInt;
            }
            return null;
        }));
    }

    public async Task<List<string>> GenerateRandomRoomsAsync(int toGenerate)
    {
        string instruction = $"You are an API that generates realistic hospital room codes. " +
                             $"Each room must consist of two uppercase letters followed by three digits, like 'AB302'. " +
                             $"Generate exactly {toGenerate} unique room codes. Separate each room code with a semicolon (;). " +
                             $"No extra text.";

        return await GenerateDataAsync(toGenerate, instruction, ParseResponseToList);
    }


}
