using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Asignment_PRN231_API_FE.Pages.Common;
using System.Text.Json.Serialization;

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
        public async Task<bool> RefreshTokenAsync()
        {
            var refreshToken = _httpContextAccessor.HttpContext.Session.GetString("RefreshToken");
            if (string.IsNullOrEmpty(refreshToken))
            {
                return false; // Nếu không có refresh token, yêu cầu đăng nhập lại
            }

            var refreshRequest = new { RefreshToken = refreshToken };
            var content = new StringContent(JsonSerializer.Serialize(refreshRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/refresh", content);
            if (!response.IsSuccessStatusCode)
            {
                return false; // Refresh thất bại => cần đăng nhập lại
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var refreshResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // 🔹 Cập nhật Token trong Session
            _httpContextAccessor.HttpContext.Session.SetString("JWTToken", refreshResponse.Token);
            _httpContextAccessor.HttpContext.Session.SetString("RefreshToken", refreshResponse.RefreshToken);

            return true;
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var loginRequest = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/login", content);
            if (!response.IsSuccessStatusCode)
            {
                // Log lỗi hoặc trả về thông báo cụ thể

                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // 🔹 Kiểm tra null trước khi tạo Claims
            if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
            {
                return null;
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

            if (!string.IsNullOrEmpty(authResponse.FullName))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, authResponse.FullName));
            }
            if (!string.IsNullOrEmpty(authResponse.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, authResponse.Email));
            }
            if (!string.IsNullOrEmpty(authResponse.Token))
            {
                claims.Add(new Claim("JWTToken", authResponse.Token));
            }
            if (authResponse.Roles?.Any() == true)
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", authResponse.Roles)));
            }
            if (!string.IsNullOrEmpty(authResponse.Avatar))
            {
                claims.Add(new Claim("Avatar", authResponse.Avatar));
                _httpContextAccessor.HttpContext.Session.SetString("Avatar", authResponse.Avatar);
            }
            if (authResponse.ShopId > 0)
            {
                claims.Add(new Claim("ShopId", authResponse.ShopId.ToString()));
                _httpContextAccessor.HttpContext.Session.SetString("ShopId", authResponse.ShopId.ToString());
            }
            if (!string.IsNullOrEmpty(authResponse.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, authResponse.Email));
                _httpContextAccessor.HttpContext.Session.SetString("Email", authResponse.Email); // 👈 Lưu email vào session
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            _httpContextAccessor.HttpContext.Session.SetString("JWTToken", authResponse.Token);

            return authResponse.Roles?.FirstOrDefault() ?? "unknow";
        }

        public async Task Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null) return;
            httpContext.Session.Clear();


            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

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
            public string FullName { get; set; }
            public List<string> Roles { get; set; }
            public string RefreshToken { get; internal set; }
            [JsonPropertyName("avatar")]
            public string Avatar { get; set; }
            public string Email { get; set; }
            public string Token { get; set; }
            public int? ShopId { get; set; }

        }
    }
}
