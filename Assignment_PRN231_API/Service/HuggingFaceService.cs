using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Assignment_PRN231_API.Service
{
    public class HuggingFaceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _modelUrl = "https://api-inference.huggingface.co/models/google/mt5-base";



        public HuggingFaceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["HuggingFace:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetBusinessAdvice(string question)
        {
            var request = new { inputs = question };

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_modelUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"❌ AI không khả dụng. Chi tiết lỗi: {responseString}";
            }

            try
            {
                using var jsonDoc = JsonDocument.Parse(responseString);
                var root = jsonDoc.RootElement;

                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var firstItem = root[0];

                    if (firstItem.TryGetProperty("generated_text", out var generatedText))
                    {
                        return generatedText.GetString() ?? "⚠️ Không có phản hồi từ AI.";
                    }
                }

                return "⚠️ API phản hồi nhưng không chứa kết quả hợp lệ.";
            }
            catch (JsonException)
            {
                return "❌ Lỗi phân tích JSON từ API.";
            }
        }
    }


}
