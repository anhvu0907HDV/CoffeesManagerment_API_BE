using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageShop
{
    public class EditModel : BasePageModel
    {
        public EditModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }
        [BindProperty]
        public ShopVM Shop { get; set; } = new ShopVM();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"shop/get-shop/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            Shop = JsonSerializer.Deserialize<ShopVM>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            return Page();
        }
    }
}
