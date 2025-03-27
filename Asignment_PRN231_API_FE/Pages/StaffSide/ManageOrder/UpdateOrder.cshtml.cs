using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asignment_PRN231_API_FE.Pages.StaffSide.ManageOrder
{
    public class UpdateOrderModel : BasePageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateOrderModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public List<ProductVM> Products { get; set; } = new();
        public List<TableVM> Tables { get; set; } = new();
        public int OrderId { get; set; }
        public string SelectedPaymentMethod { get; set; }
        public int SelectedTableId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            // Get order details
            var response = await client.GetAsync($"https://localhost:7079/staff/GetOrderById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<OrderForGetDeteailVM>();
                if (order == null)
                {
                    return NotFound(new { message = "Đơn hàng không tồn tại." });
                }

                OrderId = id;
                SelectedPaymentMethod = order.Payment.PaymentMethod;
                SelectedTableId = order.TableId;  // assuming that TableId is in the order object
            }

            // Get product list
            var productResponse = await client.GetAsync("https://localhost:7079/api/Product/get-all-product");
            if (productResponse.IsSuccessStatusCode)
            {
                Products = await productResponse.Content.ReadFromJsonAsync<List<ProductVM>>() ?? new List<ProductVM>();
            }

            // Get shop id and tables
            var userId = "e151c6fa-e91f-49fe-9a9a-a1bf373983e6"; // replace with dynamic user ID
            var shopResponse = await client.GetAsync($"https://localhost:7079/staff/get-shop-id-by-user/{userId}");
            if (shopResponse.IsSuccessStatusCode)
            {
                var shopData = await shopResponse.Content.ReadFromJsonAsync<ShopIdVM>();
                var shopId = shopData?.ShopId ?? 0;

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
