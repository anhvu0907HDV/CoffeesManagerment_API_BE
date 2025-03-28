
using Assignment_PRN231_API.DTOs.AI;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using System.Text;
using System.Text.Json;

namespace Assignment_PRN231_API.Service
{
    public class GoogleGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
        private readonly IProductRepository _productRepository;

        public GoogleGeminiService(IProductRepository productRepository, HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleGemini:ApiKey"];
            _productRepository = productRepository;
        }


        public async Task<string> GetBusinessAdvice(List<ChatMessage> conversation)
        {
            // Lấy danh sách sản phẩm từ repository
            var products = await _productRepository.GetAllProducts();
            var productList = products.Select(p => p.ProductName).ToList();

            // Chuyển danh sách sản phẩm thành chuỗi
            string productListString = string.Join(", ", productList);

            string systemPrompt = "Bạn là một chuyên gia tư vấn về đồ uống như cà phê, trà sữa, nước ép. " +
                                  "Dưới đây là danh sách đồ uống có sẵn tại cửa hàng:\n" +
                                  $"{productListString}\n\n" +
                                  "Bạn chỉ được tư vấn các món trong danh sách trên. " +
                                  "Nếu người dùng hỏi món không có trong danh sách, hãy từ chối lịch sự.";

            // Định dạng lại cuộc trò chuyện
            var formattedConversation = conversation.Select(msg => new
            {
                parts = new[] { new { text = msg.Role == "user" ? $"{systemPrompt}\n\nNgười dùng hỏi: {msg.Text}" : msg.Text } },
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
