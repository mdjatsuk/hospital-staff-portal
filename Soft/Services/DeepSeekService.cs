using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class DeepSeekService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public DeepSeekService(IConfiguration config)
    {
        _http = new HttpClient();
        _apiKey = config["OpenAI:ApiKey"] ?? throw new Exception("OpenAI API key not found.");
    }

    public async Task<string> GenerateRandomNameAsync()
    {
        var request = new
        {
            model = "deepseek-ai/DeepSeek-V",
            messages = new[]
            {
                new { role = "system", content = "Generate a realistic random person's full name (first and last). Reply with only the name." },
                new { role = "user", content = "Give me a random name." }
            },
            temperature = 0.7
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _http.PostAsync("https://api.deepinfra.com/v1/openai/chat/completions", content);
        var responseString = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseString);
        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()
            ?.Trim() ?? "John Doe";
    }
}