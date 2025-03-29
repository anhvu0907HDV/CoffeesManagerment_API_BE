using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageShop
{
    [Authorize(Roles = "Owner")]
    public class EditModel : BasePageModel
    {
        public EditModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }
        [BindProperty]
        public ShopVM Shop { get; set; } = new ShopVM();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }
            var response = await _httpClient.GetAsync($"shop/get-shop/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            Shop = JsonSerializer.Deserialize<ShopVM>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateError());
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }


            var formData = new MultipartFormDataContent
            {
                { new StringContent(Shop.ShopId.ToString()!), "ShopId" },
                { new StringContent(Shop.Name ?? ""), "Name" },
                { new StringContent(Shop.Address ?? ""), "Address" },
                { new StringContent(Shop.PhoneNumber ?? ""), "PhoneNumber" }

            };

            var response = await httpClient.PutAsync($"shop/update-shop/{Shop.ShopId}", formData);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Cập nhật thông tin cửa hàng thất bại!");
                return Page();
            }
            TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateSuccess());
            return RedirectToPage("Index");
        }
    }
}
