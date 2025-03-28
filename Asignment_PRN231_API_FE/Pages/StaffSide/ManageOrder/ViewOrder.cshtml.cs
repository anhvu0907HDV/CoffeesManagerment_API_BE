using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asignment_PRN231_API_FE.Pages.StaffSide.ManageOrder
{
    public class ViewOrderModel : BasePageModel
	{
		public ViewOrderModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
			: base(httpContextAccessor, authService, httpClientFactory)
		{
		}

		public List<OrderVM> Orders { get; set; } = new();
		[BindProperty(SupportsGet = true)]
		public string? SelectedStatus { get; set; }
        [BindProperty]
        public int CancelOrderId { get; set; }
        public List<SelectListItem> OrderStatuses { get; set; } = new()
		{
			new SelectListItem("Pending", "Pending"),
			new SelectListItem("Completed", "Completed"),
            new SelectListItem("Canceled", "Canceled"),
            new SelectListItem("Prepared", "Prepared")

        };

		public async Task<IActionResult> OnGetAsync()
		{
			var client = await GetAuthorizedHttpClientAsync();
			if (client == null) return RedirectToPage("/Authentication/Login");

			var userId = "e151c6fa-e91f-49fe-9a9a-a1bf373983e6";//_httpContextAccessor.HttpContext.Session.GetString("UserId");
			if (string.IsNullOrEmpty(userId)) return Unauthorized();

			var response = await client.GetAsync($"https://localhost:7079/staff/GetOrdersByUserId/{userId}");
			if (!response.IsSuccessStatusCode) return Page();

			var allOrders = await response.Content.ReadFromJsonAsync<List<OrderVM>>() ?? new();

			if (!string.IsNullOrEmpty(SelectedStatus))
			{
				Orders = allOrders.Where(o => o.OrderStatus == SelectedStatus).ToList();
			}
			else
			{
				Orders = allOrders;
			}

            foreach (var order in allOrders)
            {
                var tableResponse = await client.GetAsync($"https://localhost:7079/staff/get-tables-by-order/{order.OrderId}");
                if (tableResponse.IsSuccessStatusCode)
                {
                    var tables = await tableResponse.Content.ReadFromJsonAsync<List<TableVM>>();
                    order.TableName = tables?.FirstOrDefault()?.Name ?? "(Không có bàn)";
                }
            }

            return Page();
		}

        public async Task<IActionResult> OnPostCancelAsync()
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            var body = new
            {
                orderStatus = "Canceled",
                paymentStatus = "Canceled"
            };

            var response = await client.PutAsJsonAsync($"https://localhost:7079/staff/update-order-status/{CancelOrderId}", body);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Đơn hàng đã được hủy.";
            }
            else
            {
                TempData["ErrorMessage"] = "Đã có lỗi xảy ra khi hủy đơn hàng.";
            }

            return RedirectToPage(); // Tải lại trang
        }

    }
}
