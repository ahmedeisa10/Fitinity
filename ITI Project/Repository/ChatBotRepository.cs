using System.Text.Json;
using System.Text;

namespace ITI_Project.Repository
{
    public class ChatBotRepository : IChatBotRepository
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;

        public ChatBotRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/");

            
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<string> AskGeminiAsync(string message)
        {
            var request = new
            {
                contents = new[]
                {
            new {
                parts = new[] {
                    new { text = "You are a strict assistant. Answer only about diet plans, healthy food recommendations, sports, and what to eat to gain weight. Always respond with short, direct answers only. No introductions, no explanations. For anything else reply with exactly: Not supported." }
                }
            },
            new {
                parts = new[] {
                    new { text = message }
                }
            }
        },
                generationConfig = new
                {
                    maxOutputTokens = 30,   
                    temperature = 0.3,      
                    topP = 0.8
                }
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"models/gemini-2.5-pro:generateContent?key={_apiKey}", content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var reply = doc.RootElement
                           .GetProperty("candidates")[0]
                           .GetProperty("content")
                           .GetProperty("parts")[0]
                           .GetProperty("text")
                           .GetString();

            return reply ?? "No response";
        }
    }
}

