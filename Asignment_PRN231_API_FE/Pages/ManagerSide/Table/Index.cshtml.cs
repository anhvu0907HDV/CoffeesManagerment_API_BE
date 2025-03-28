using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
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

        [BindProperty] public TableVM NewTable { get; set; } = new();
        [BindProperty] public TableVM EditTable { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            var response = await httpClient.GetAsync($"api/tables/shop/{ShopId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Tables = JsonSerializer.Deserialize<List<TableVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            NewTable.ShopId = ShopId;

            var json = JsonSerializer.Serialize(NewTable);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/tables", content);
            TempData["Toast"] = response.IsSuccessStatusCode
                ? JsonSerializer.Serialize(Toast.CreateSuccess())
                : JsonSerializer.Serialize(Toast.CreateError());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            var json = JsonSerializer.Serialize(EditTable);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"api/tables/{EditTable.TableId}", content);
            TempData["Toast"] = response.IsSuccessStatusCode
                ? JsonSerializer.Serialize(Toast.UpdateSuccess())
                : JsonSerializer.Serialize(Toast.UpdateError());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int tableId)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            var response = await httpClient.DeleteAsync($"api/tables/{tableId}");
            TempData["Toast"] = response.IsSuccessStatusCode
                ? JsonSerializer.Serialize(Toast.DeleteSuccess())
                : JsonSerializer.Serialize(Toast.DeleteError());

            return RedirectToPage();
        }

    }
}
