using Assignment_PRN231_API.DTOs.AI;
using Assignment_PRN231_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("ai")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly GoogleGeminiService _geminiService;

        public AIController(GoogleGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("business-advice")]
        public async Task<IActionResult> GetBusinessAdvice([FromBody] ChatRequest request)
        {
            if (request.Conversation == null || request.Conversation.Count == 0)
                return BadRequest("Hội thoại không hợp lệ.");

            var advice = await _geminiService.GetBusinessAdvice(request.Conversation);
            return Ok(new { advice });
        }

        // Model dữ liệu
        public class ChatRequest
        {
            public List<ChatMessage> Conversation { get; set; }
        }

 
    }
}
