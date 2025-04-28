using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenAiService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public OpenAiService(IConfiguration config)
    {
        _http = new HttpClient();
        _apiKey = config["OpenAI:ApiKey"] ?? throw new Exception("OpenAI API key not found.");

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<List<string>> GenerateRandomNamesAsync(int namesToRequest)
    {
        var names = new List<string>();
        int namesPerRequest = 1000; // Always maximum 1000 per request
        int remainingNames = namesToRequest;

        while (remainingNames > 0)
        {
            int currentBatchSize = Math.Min(remainingNames, namesPerRequest);

            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new
                {
                    role = "system",
                    content = $"You are a machine API. You must generate exactly {currentBatchSize} random realistic full names (first name and last name), separated by commas. No numbering, no bullets, no newlines, no extra text or explanations. Only output a single line of names separated by commas. Failure to follow exactly will result in task rejection."
                },
                new
                {
                    role = "user",
                    content = $"Generate exactly {currentBatchSize} random realistic full names (first and last names), separated by commas. No extra text, no newlines, no numbering, only the names."
                }
            },
                temperature = 0.7,
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _http.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(responseString);
                var responseNames = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString()?
                    .Trim()
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => n.Trim())
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList();

                if (responseNames != null)
                {
                    names.AddRange(responseNames);
                }
            }
            else
            {
                throw new Exception("Failed to generate names: " + responseString);
            }

            remainingNames = namesToRequest - names.Count;

            if (remainingNames > 0)
            {
                Console.WriteLine($"Generated {names.Count}. Still missing {remainingNames} names, retrying...");
            }
            else if (remainingNames < 0)
            {
                // Too many names generated, cut the list
                names = names.Take(namesToRequest).ToList();
                break;
            }
        }

        return names;
    }
}
