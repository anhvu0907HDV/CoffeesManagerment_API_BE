using Asignment_PRN231_API_FE.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Asignment_PRN231_API_FE.Pages.Common
{
    public class BasePageModel : PageModel
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthService _authService;
        private readonly IHttpClientFactory _httpClientFactory;

        protected HttpClient _httpClient;

        public BasePageModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("API");
        }

        protected async Task<HttpClient?> GetAuthorizedHttpClientAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                bool refreshed = await _authService.RefreshTokenAsync();
                if (!refreshed)
                {
                    _httpContextAccessor.HttpContext?.Session.Clear();
                    return null; // Trả về null để xử lý redirect ở PageModel kế thừa
                }
                accessToken = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
            }

            // Reset Authorization Header mỗi lần gọi
            _httpClient = _httpClientFactory.CreateClient("API");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return _httpClient;
        }
    }
}
