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

        public async Task<IActionResult> OnGetAsync()
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            var response = await client.GetAsync("https://localhost:7079/api/Product/get-all-product");
            if (response.IsSuccessStatusCode)
            {
                Products = await response.Content.ReadFromJsonAsync<List<ProductVM>>() ?? new List<ProductVM>();
            }

            return Page();
        }
        // Handle POST from JS to create Order
        public async Task<IActionResult> OnPostAsync([FromBody] CreateOrderPostVM orderData)
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return Unauthorized();

            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var createOrderDto = new
            {
                UserId = userId,
                TableId = 1, // fix cứng tạm
                PaymentMethod = "Cash", // fix cứng tạm
                PaymentId = Guid.NewGuid().ToString(), // hoặc có thể tạo Payment trước
                OrderDetails = orderData.OrderDetails
            };

            var json = JsonSerializer.Serialize(createOrderDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/staff/createOrder", content);
            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false, message = "Lỗi khi tạo đơn hàng." });
        }

        public class CreateOrderPostVM
        {
            public List<OrderDetailInput> OrderDetails { get; set; } = new();
        }

        public class OrderDetailInput
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
