using MVC.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

    public async Task<List<(string FullName, Genders Gender)>> GenerateRandomNamesWithGendersAsync(int toGenerate)
    {
        var result = new List<(string FullName, Genders Gender)>();

        int generatedCount = 0;
        int batchSize = Math.Min(toGenerate * 2, MaxBatchSize);
        int remainingNames = batchSize;

        while (remainingNames > 0)
        {
            int requestCount = Math.Min(remainingNames, MaxBatchSize);

            var requestPayload = BuildOpenAiRequest(requestCount);
            var responseString = await SendOpenAiRequestAsync(requestPayload);

            if (string.IsNullOrWhiteSpace(responseString))
                continue;

            var parsed = TryParseResponse(responseString, out List<(string FullName, Genders Gender)> parsedEntries);

            if (!parsed)
                continue;

            foreach (var entry in parsedEntries)
            {
                result.Add(entry);
                generatedCount++;
                if (generatedCount >= batchSize)
                    break;
            }

            remainingNames = batchSize - generatedCount;

            if (remainingNames > 0)
            {
                Console.WriteLine($"Generated {generatedCount}. Still missing {remainingNames} names, retrying...");
            }
        }

        return result.Take(batchSize).ToList();
    }

    private object BuildOpenAiRequest(int count)
    {
        string instruction = $"You are an API that generates random full names (first and last) with gender (0 for Male, 1 for Female). " +
                             $"Generate exactly {count} full names, each followed by a comma and gender. " +
                             $"Separate each entry with a semicolon (;). No extra text. Exact count required.";

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

        using var doc = JsonDocument.Parse(responseBody);
        return doc.RootElement
                  .GetProperty("choices")[0]
                  .GetProperty("message")
                  .GetProperty("content")
                  .GetString()?
                  .Trim();
    }

    private bool TryParseResponse(string response, out List<(string FullName, Genders Gender)> entries)
    {
        entries = new();
        var rawEntries = response.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawEntry in rawEntries)
        {
            var parts = rawEntry.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                continue;

            var name = parts[0].Trim();
            var genderStr = parts[1].Trim();

            if (int.TryParse(genderStr, out int genderInt) && Enum.IsDefined(typeof(Genders), genderInt))
            {
                entries.Add((name, (Genders)genderInt));
            }
        }

        return entries.Count > 0;
    }
}
