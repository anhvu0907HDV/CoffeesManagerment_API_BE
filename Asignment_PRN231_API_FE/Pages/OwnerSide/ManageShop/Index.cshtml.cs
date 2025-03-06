using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageShop
{

    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
           
        }
        public List<ShopOwnerVM> Shops { get; set; } = new();
        public async Task<IActionResult> OnGet()
        {
            var response = await _httpClient.GetAsync("owner/get-all-shop");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            Shops = JsonSerializer.Deserialize<List<ShopOwnerVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }
    }
}
