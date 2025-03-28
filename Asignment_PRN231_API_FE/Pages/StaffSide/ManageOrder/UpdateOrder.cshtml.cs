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
        public string PaymentId { get; set; }

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
                PaymentId = order.Payment.PaymentId;
            }

            // Get product list
            var productResponse = await client.GetAsync("https://localhost:7079/api/Product/get-all-product");
            if (productResponse.IsSuccessStatusCode)
            {
                Products = await productResponse.Content.ReadFromJsonAsync<List<ProductVM>>() ?? new List<ProductVM>();
            }

            return Page();
        }
    }
}
