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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            // Gọi API lấy thông tin chi tiết đơn hàng
            var response = await client.GetAsync($"https://localhost:7079/staff/GetOrderById/{id}");
            if (response.IsSuccessStatusCode)
            {
                Order = await response.Content.ReadFromJsonAsync<OrderForGetDeteailVM>();
            }

            if (Order == null)
            {
                return NotFound(new { message = "Đơn hàng không tồn tại." });
            }


            return Page();
        }
    }
}
