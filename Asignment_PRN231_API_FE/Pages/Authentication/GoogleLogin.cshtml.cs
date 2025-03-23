using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class GoogleLoginModel : BasePageModel
    {
        public GoogleLoginModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        [BindProperty]
        public string IdToken { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(IdToken))
            {
                ModelState.AddModelError(string.Empty, "Google login failed.");
                return Page();
            }

            var requestContent = new StringContent(JsonSerializer.Serialize(new { idToken = IdToken }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/google-login", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Login failed.");
                return Page();
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // 🔹 Kiểm tra null trước khi tạo Claims
            if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
            {
                return null!;
            }

            var claims = new List<Claim>();

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
                _httpContextAccessor.HttpContext!.Session.SetString("Avatar", authResponse.Avatar);
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            _httpContextAccessor.HttpContext!.Session.SetString("JWTToken", authResponse.Token);

            return RedirectToPage("/Index");
        }
        private class AuthResponse
        {
            public string? FullName { get; set; }
            public List<string>? Roles { get; set; }
            public string? RefreshToken { get; internal set; }
            [JsonPropertyName("avatar")]
            public string? Avatar { get; set; }
            public string? Email { get; set; }
            public string? Token { get; set; }

        }
    }
}
