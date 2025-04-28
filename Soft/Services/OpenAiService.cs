using MVC.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenAiService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    private const int MaxBatchSize = 1000; // Maximum number of names per request

    public OpenAiService(IConfiguration config)
    {
        _http = new HttpClient();
        _apiKey = config["OpenAI:ApiKey"] ?? throw new Exception("OpenAI API key not found.");

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<List<(string FullName, Genders Gender)>> GenerateRandomNamesWithGendersAsync(int batchSize)
    {
        var result = new List<(string FullName, Genders Gender)>();
        int generatedCount = 0;

        // Ensure the batch size does not exceed the maximum allowed
        batchSize = Math.Min(batchSize, MaxBatchSize);

        int remainingNames = batchSize;

        while (remainingNames > 0)
        {
            var requestBatchSize = remainingNames;

            // Make sure we do not request more than the maximum allowed batch size
            requestBatchSize = Math.Min(requestBatchSize, MaxBatchSize);

            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new
                {
                    role = "system",
                    content = $"You are an API. Generate exactly {requestBatchSize} random full names (first name and last name), followed by a comma and 0 for Male or 1 for Female. Separate each entry by a semicolon (;). No extra text."
                },
                new
                {
                    role = "user",
                    content = $"Generate exactly {requestBatchSize} full names with gender 0 (Male) or 1 (Female) after a comma, separated by semicolons (;)."
                }
            },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI request failed: {responseString}");
            }

            using var doc = JsonDocument.Parse(responseString);
            var rawText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()?
                .Trim();

            if (string.IsNullOrEmpty(rawText))
            {
                continue; // Retry if no names are returned
            }

            var entries = rawText.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var entry in entries)
            {
                var parts = entry.Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    var fullName = parts[0].Trim();
                    var genderValue = parts[1].Trim();

                    if (int.TryParse(genderValue, out int genderInt) && Enum.IsDefined(typeof(Genders), genderInt))
                    {
                        var gender = (Genders)genderInt;
                        result.Add((fullName, gender));
                        generatedCount++;

                        if (generatedCount >= batchSize)
                        {
                            break; // Stop once we've reached the requested batch size
                        }
                    }
                }
            }

            // Adjust remaining names
            remainingNames = batchSize - generatedCount;

            if (remainingNames > 0)
            {
                Console.WriteLine($"Generated {generatedCount}. Still missing {remainingNames} names, retrying...");
            }
            else if (remainingNames < 0)
            {
                // Too many names generated, cut the list
                result = result.Take(batchSize).ToList();
                break; // Stop after cutting off excess
            }
        }

        // Return the final list with the exact requested number of names
        return result;
    }

}
