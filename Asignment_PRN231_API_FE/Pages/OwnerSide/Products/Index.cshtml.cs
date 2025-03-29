using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.Products
{
    [Authorize(Roles = "Owner")]
    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }

        public List<ListProductVM> Products { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            try
            {
                Products = await httpClient.GetFromJsonAsync<List<ListProductVM>>("api/product/get-all-product") ?? new List<ListProductVM>();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Lỗi khi tải danh sách sản phẩm.");
            }

            return Page();
        }
    }

}
