using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageShop
{

    [Authorize(Roles = "Owner")]
    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
           
        }
        public List<ShopOwnerVM> Shops { get; set; } = new();
        public async Task<IActionResult> OnGet()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }
            var response = await _httpClient.GetAsync("owner/get-all-shop");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            Shops = JsonSerializer.Deserialize<List<ShopOwnerVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int shopId)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var response = await httpClient.DeleteAsync($"shop/delete-shop/{shopId}");

            var shops = await _httpClient.GetAsync("owner/get-all-shop");

            if (!shops.IsSuccessStatusCode)
            {
                return Page();
            }

            var json = await shops.Content.ReadAsStringAsync();
            Shops = JsonSerializer.Deserialize<List<ShopOwnerVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            TempData["Toast"] = JsonSerializer.Serialize(Toast.DeleteError());

            if (!response.IsSuccessStatusCode)
            {
               
                return RedirectToPage("Index");
            }

            TempData["Toast"] = JsonSerializer.Serialize(Toast.DeleteSuccess());
            
            return RedirectToPage("Index");
        }
    }
}
