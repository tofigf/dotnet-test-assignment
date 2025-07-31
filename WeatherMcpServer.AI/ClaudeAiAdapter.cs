using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WeatherMcpServer.AI
{
    public class ClaudeAiAdapter : IAiAdapter
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        public string Name => "Claude";

        public ClaudeAiAdapter(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["Claude:ApiKey"] ?? throw new Exception("Missing Claude API key");
        }

        public async Task<string> GenerateSummaryAsync(string prompt, CancellationToken cancellationToken)
        {
            var payload = new
            {
                model = "claude-3-opus-20240229",
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 300
            };

            var req = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages")
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            req.Headers.Add("anthropic-version", "2023-06-01");

            var res = await _http.SendAsync(req, cancellationToken);
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("content")[0].GetProperty("text").GetString() ?? "No response";
        }
    }
}
