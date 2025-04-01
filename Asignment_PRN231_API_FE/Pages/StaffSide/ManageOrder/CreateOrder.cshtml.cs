using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Asignment_PRN231_API_FE.Pages.StaffSide.ManageOrder
{
    [Authorize(Roles = "Staff")]
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

            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Không tìm thấy email người dùng.");
                return Page();
            }

            // 🔹 Gọi API để lấy UserId theo Email
            var userIdResponse = await client.GetAsync($"staff/get-user-id?email={email}");
            if (!userIdResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi rồi!!!!!!!!!!!!!!!!!!");
                ModelState.AddModelError("", "Không thể lấy thông tin người dùng.");
                return Page();
            }

            var userIdData = await userIdResponse.Content.ReadFromJsonAsync<UserIdResponse>();
            var userId_raw = userIdData?.UserId;
            if (userId_raw == null)
            {
                ModelState.AddModelError("", "Không tìm thấy người dùng tương ứng.");
                return Page();
            }

            // Lấy sản phẩm
            var response = await client.GetAsync("api/Product/get-all-product");
            if (response.IsSuccessStatusCode)
            {
                Products = await response.Content.ReadFromJsonAsync<List<ProductVM>>() ?? new List<ProductVM>();
            }

            // Lấy ShopId theo UserId
            var userId = userId_raw; // Lấy từ session hoặc input đăng nhập
            var shopResponse = await client.GetAsync($"staff/get-shop-id-by-user/{userId}");
            if (shopResponse.IsSuccessStatusCode)
            {
                var shopData = await shopResponse.Content.ReadFromJsonAsync<ShopIdVM>();
                var shopId = shopData?.ShopId ?? 0;
                // Lấy Table theo ShopId
                var tableResponse = await client.GetAsync($"staff/get-tables-by-shop/{shopId}");
                if (tableResponse.IsSuccessStatusCode)
                {
                    var tableData = await tableResponse.Content.ReadFromJsonAsync<TableResponse>();
                    Tables = tableData?.Tables.Where(t => t.Status == false).ToList() ?? new List<TableVM>();
                }
            }

            return Page();
        }

        // OnPost method to handle form submission
        public async Task<IActionResult> OnPostAsync(string tableId, string paymentMethod, string orderDetails)
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Không tìm thấy email người dùng.");
                return Page();
            }

            // 🔹 Gọi API để lấy UserId theo Email
            var userIdResponse = await client.GetAsync($"staff/get-user-id?email={email}");
            if (!userIdResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi rồi!!!!!!!!!!!!!!!!!!");
                ModelState.AddModelError("", "Không thể lấy thông tin người dùng.");
                return Page();
            }

            var userIdData = await userIdResponse.Content.ReadFromJsonAsync<UserIdResponse>();
            var userId_raw = userIdData?.UserId;
            if (userId_raw == null)
            {
                ModelState.AddModelError("", "Không tìm thấy người dùng tương ứng.");
                return Page();
            }

            // Giải mã JSON OrderDetails thành danh sách các sản phẩm
            var orderDetailsList = JsonSerializer.Deserialize<List<OrderDetailInputDto>>(orderDetails, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Console.WriteLine(JsonSerializer.Serialize(orderDetailsList));
            if (orderDetailsList == null || !orderDetailsList.Any())
            {
                Console.WriteLine("Bạn chưa chọn sản phẩm nào.");
                ModelState.AddModelError("", "Bạn chưa chọn sản phẩm nào.");
                return Page(); // Trả về trang với thông báo lỗi
            }

            // Prepare body for creating the order
            var orderDto = new
            {
                userId = userId_raw, // Lấy từ session hoặc input đăng nhập của người dùng
                tableId = tableId,
                orderDetails = orderDetailsList,
                paymentMethod = paymentMethod
            };

            var response = await client.PostAsJsonAsync("staff/createOrder", orderDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/StaffSide/ManageOrder/ViewOrder"); // Điều hướng đến trang xem đơn hàng
            }

            ModelState.AddModelError("", "Có lỗi xảy ra khi tạo đơn hàng.");
            return Page();
        }


        public class OrderDetailInputDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
        public class TableResponse
        {
            public List<TableVM> Tables { get; set; } = new();
        }
    }
 }


