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
    }

    public async Task<List<string>> GenerateRandomNamesAsync(int namesToRequest)
    {
        var names = new List<string>();
        int remainingNames = namesToRequest;

        while (remainingNames > 0)
        {
            int currentBatchSize = CalculateDynamicBatchSize(remainingNames);

            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Reply ONLY with a list of full names separated by commas. DO NOT number them, just names." },
                    new { role = "user", content = $"Generate {currentBatchSize} random full names (first and last name), separated by commas. Do not number them!" }
                },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await SendRequestWithRetriesAsync(content);

            using var doc = JsonDocument.Parse(response);
            var responseNames = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()?
                .Trim()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (responseNames != null)
            {
                names.AddRange(responseNames.Select(n => n.Trim()));
            }

            remainingNames -= currentBatchSize;
        }

        return names.Take(namesToRequest).ToList(); // Guarantee exact count
    }

    private int CalculateDynamicBatchSize(int remainingNames)
    {
        if (remainingNames <= 5)
            return remainingNames;
        else if (remainingNames <= 20)
            return 5;
        else if (remainingNames <= 50)
            return 10;
        else
            return 15;
    }

    private async Task<string> SendRequestWithRetriesAsync(HttpContent content)
    {
        int maxRetries = 3;
        int delayMilliseconds = 2000; // start with 2 seconds

        for (int attempt = 0; attempt <= maxRetries; attempt++)
        {
            var response = await _http.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests || responseString.Contains("rate_limit_exceeded"))
            {
                if (attempt == maxRetries)
                    throw new Exception("Rate limit exceeded and retries exhausted: " + responseString);

                await Task.Delay(delayMilliseconds);
                delayMilliseconds *= 2; // exponential backoff
            }
            else
            {
                throw new Exception("Failed to generate names: " + responseString);
            }
        }

        throw new Exception("Unexpected error during retries.");
    }
}
