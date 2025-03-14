using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageShop
{
    public class AddModel : BasePageModel
    {
        public AddModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        [BindProperty]
        public ShopVM Shop { get; set; } = new ShopVM();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateError());

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
            { new StringContent(Shop.Name ?? ""), "Name" },
            { new StringContent(Shop.Address ?? ""), "Address" },
            { new StringContent(Shop.PhoneNumber ?? ""), "PhoneNumber" }
        };

            var response = await httpClient.PostAsync("shop/create-shop", formData);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Thêm cửa hàng thất bại!");
                return Page();
            }

            TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateSuccess());
            return RedirectToPage("Index");
        }
    }
}
