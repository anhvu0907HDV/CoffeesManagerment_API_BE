using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net.Http;

namespace Asignment_PRN231_API_FE.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginRequest = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/login", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            _httpContextAccessor.HttpContext.Session.SetString("JWTToken", authResponse.Token); // Lưu token vào Session

            _httpContextAccessor.HttpContext.Session.SetString("UserRoles", JsonSerializer.Serialize(authResponse.Roles));
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("JWTToken", authResponse.Token), // Lưu token vào claims để sử dụng
                    new Claim (ClaimTypes.Role, string.Join(",", authResponse.Roles))
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Ghi nhớ đăng nhập nếu cần
            };

            // **Thiết lập Authentication**
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return true;
        }

        public async Task Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Session.Remove("JWTToken");
        }

        public string GetToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
        }
        public async Task<bool> IsInRoleAsync(string role)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return false;

            // Lấy danh sách Role từ Session
            var rolesJson = httpContext.Session.GetString("UserRoles");
            if (string.IsNullOrEmpty(rolesJson))
                return false;

            var roles = JsonSerializer.Deserialize<List<string>>(rolesJson);
            return roles.Contains(role);
        }
        public async Task<bool> IsAuthenticatedAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return false;

            return httpContext.User.Identity?.IsAuthenticated ?? false;
        }

        private class AuthResponse
        {
            public string Token { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}
