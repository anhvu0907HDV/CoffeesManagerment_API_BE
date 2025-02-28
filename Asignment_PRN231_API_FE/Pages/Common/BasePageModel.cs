using Asignment_PRN231_API_FE.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace Asignment_PRN231_API_FE.Pages.Common
{
    public class BasePageModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;

        public BasePageModel(IHttpContextAccessor httpContextAccessor, AuthService authService, HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _httpClient = httpClient;
        }

        protected async Task<HttpClient> GetAuthorizedHttpClientAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                bool refreshed = await _authService.RefreshTokenAsync();
                if (!refreshed)
                {
                    _httpContextAccessor.HttpContext.Session.Clear();
                    Response.Redirect("/Login");
                    return null;
                }
                accessToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return _httpClient;
        }
    }
}
