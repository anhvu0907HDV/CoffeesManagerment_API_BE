using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asignment_PRN231_API_FE.Pages.StaffSide.ManageOrder
{
    public class ViewDetailOrderModel : BasePageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ViewDetailOrderModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public OrderForGetDeteailVM Order { get; set; }

        [BindProperty]
        public int OrderId { get; set; }

        [BindProperty]
        public string CurrentStatus { get; set; } = "";
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            // Gọi API lấy thông tin chi tiết đơn hàng
            var response = await client.GetAsync($"https://localhost:7079/staff/GetOrderById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng." });
            }

            Order = await response.Content.ReadFromJsonAsync<OrderForGetDeteailVM>();
            if (Order == null)
            {
                return NotFound(new { message = "Đơn hàng không tồn tại." });
            }

            // Gọi API lấy thông tin table theo orderId
            var tableResponse = await client.GetAsync($"https://localhost:7079/staff/get-tables-by-order/{id}");
            if (tableResponse.IsSuccessStatusCode)
            {
                var tables = await tableResponse.Content.ReadFromJsonAsync<List<TableVM>>();
                Order.Table = tables?.FirstOrDefault(); // Gán bàn đầu tiên nếu có
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            // Gọi API cập nhật Order
            var orderStatus = CurrentStatus == "Pending" ? "Prepared" : "Completed";
            var paymentStatus = CurrentStatus == "Pending" ? "Pending" : "Paid";

            var updateOrderResponse = await client.PutAsJsonAsync(
                $"https://localhost:7079/staff/update-order-status/{OrderId}",
                new
                {
                    orderStatus = orderStatus,
                    paymentStatus = paymentStatus
                });

            if (!updateOrderResponse.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "❌ Không thể cập nhật trạng thái đơn hàng.";
                return RedirectToPage(new { id = OrderId });
            }

            // Nếu vừa thanh toán xong, kiểm tra đơn còn lại theo bàn
            if (orderStatus == "Completed")
            {
                // Lấy thông tin order để lấy TableId
                var orderDetailResponse = await client.GetAsync($"https://localhost:7079/staff/GetOrderById/{OrderId}");
                if (!orderDetailResponse.IsSuccessStatusCode) return RedirectToPage(new { id = OrderId });

                var order = await orderDetailResponse.Content.ReadFromJsonAsync<OrderForGetDeteailVM>();
                var tableId = order?.Table?.TableId ?? 0;

                // Lấy các order theo bàn
                var orderByTableResponse = await client.GetAsync($"https://localhost:7079/staff/get-orders-by-table/{tableId}");
                if (orderByTableResponse.IsSuccessStatusCode)
                {
                    var orders = await orderByTableResponse.Content.ReadFromJsonAsync<List<OrderVM>>();
                    var activeOrders = orders.Where(o => o.OrderStatus != "Completed" && o.OrderStatus != "Canceled").ToList();

                    if (!activeOrders.Any())
                    {
                        // Không còn order nào => cập nhật trạng thái bàn về false
                        await client.PutAsJsonAsync(
                            $"https://localhost:7079/staff/update-table-status/{tableId}",
                            new { status = false });
                    }
                }
            }

            TempData["SuccessMessage"] = "✅ Cập nhật thành công!";
            return RedirectToPage(new { id = OrderId });
        }
    }
}
