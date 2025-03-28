using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace Asignment_PRN231_API_FE.Pages.StaffSide.ManageOrder
{
    public class CreateOrderModel : BasePageModel
    {
        public CreateOrderModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
        : base(httpContextAccessor, authService, httpClientFactory)
        {
        }


        public List<ProductVM> Products { get; set; } = new();
        public List<TableVM> Tables { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            // Lấy sản phẩm
            var response = await client.GetAsync("https://localhost:7079/api/Product/get-all-product");
            if (response.IsSuccessStatusCode)
            {
                Products = await response.Content.ReadFromJsonAsync<List<ProductVM>>() ?? new List<ProductVM>();
            }

            // Lấy ShopId theo UserId
            var userId = "e151c6fa-e91f-49fe-9a9a-a1bf373983e6"; // Lấy từ session hoặc input đăng nhập
            var shopResponse = await client.GetAsync($"https://localhost:7079/staff/get-shop-id-by-user/{userId}");
            if (shopResponse.IsSuccessStatusCode)
            {
                var shopData = await shopResponse.Content.ReadFromJsonAsync<ShopIdVM>();
                var shopId = shopData?.ShopId ?? 0;
                // Lấy Table theo ShopId
                var tableResponse = await client.GetAsync($"https://localhost:7079/staff/get-tables-by-shop/{shopId}");
                if (tableResponse.IsSuccessStatusCode)
                {
                    var tableData = await tableResponse.Content.ReadFromJsonAsync<TableResponse>();
                    Tables = tableData?.Tables ?? new List<TableVM>();
                }
            }

            return Page();
        }
        public class TableResponse
        {
            public List<TableVM> Tables { get; set; } = new();
        }
    }
}
