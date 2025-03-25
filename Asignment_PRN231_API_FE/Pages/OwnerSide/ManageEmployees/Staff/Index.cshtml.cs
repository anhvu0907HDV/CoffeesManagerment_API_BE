using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Staff
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        [BindProperty]
        public List<StaffListVM> Staffs { get; set; } = new List<StaffListVM>();
        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }
            var response = await _httpClient.GetAsync("owner/get-all-staff");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Staffs = JsonSerializer.Deserialize<List<StaffListVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return Page();
        }
    }
}
