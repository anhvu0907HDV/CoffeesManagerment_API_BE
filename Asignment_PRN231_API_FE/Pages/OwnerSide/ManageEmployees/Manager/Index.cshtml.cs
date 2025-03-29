using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Manager
{
    [Authorize(Roles = "Owner")]
    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        public List<ManagerVM> Managers { get; set; } = new();


        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");  
            }

            var response = await _httpClient.GetAsync("owner/get-all-manager");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Managers = JsonSerializer.Deserialize<List<ManagerVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToPage("/Authentication/Login"); // Nếu bị 401, yêu cầu đăng nhập lại
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid(); // Nếu bị 403, chặn truy cập
            }
            return Page();
        }
    }
}
