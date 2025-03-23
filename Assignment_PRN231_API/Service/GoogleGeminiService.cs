
using Assignment_PRN231_API.DTOs.AI;
using System.Text;
using System.Text.Json;

namespace Assignment_PRN231_API.Service
{
    public class GoogleGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";


        public GoogleGeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleGemini:ApiKey"];
        }


        public async Task<string> GetBusinessAdvice(List<ChatMessage> conversation)
        {
            string systemPrompt = "Bạn là một chuyên gia tư vấn về đồ uống như cà phê, trà sữa, nước ép. " +
                                  "Bạn chỉ được trả lời các câu hỏi liên quan đến chọn đồ uống. " +
                                  "Nếu câu hỏi không liên quan, hãy từ chối lịch sự.";

            // Thêm hướng dẫn vào mỗi tin nhắn từ người dùng
            var formattedConversation = conversation.Select(msg => new
            {
                parts = new[]
                {
            new
            {
                text = msg.Role == "user"
                    ? $"{systemPrompt}\n\nNgười dùng hỏi: {msg.Text}"  // Thêm hướng dẫn vào mỗi câu hỏi
                    : msg.Text
            }
        },
                role = msg.Role == "user" ? "user" : "model"
            }).ToList();

            var requestBody = new { contents = formattedConversation };
            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}?key={_apiKey}", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"❌ AI không khả dụng. Chi tiết lỗi: {responseString}";
            }

            try
            {
                using var jsonDoc = JsonDocument.Parse(responseString);
                var root = jsonDoc.RootElement;

                if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    var firstCandidate = candidates[0];

                    if (firstCandidate.TryGetProperty("content", out var contentProp) &&
                        contentProp.TryGetProperty("parts", out var parts) &&
                        parts.GetArrayLength() > 0)
                    {
                        return parts[0].GetProperty("text").GetString() ?? "⚠️ Không có phản hồi từ AI.";
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
