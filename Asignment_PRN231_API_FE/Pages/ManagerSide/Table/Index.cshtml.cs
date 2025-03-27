using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.ManagerSide.Table
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }

        [BindProperty(SupportsGet = true)]
        public int ShopId { get; set; }

        public List<TableVM> Tables { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var response = await httpClient.GetAsync($"api/tables/shop/{ShopId}");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            Tables = JsonSerializer.Deserialize<List<TableVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }
    }

}
