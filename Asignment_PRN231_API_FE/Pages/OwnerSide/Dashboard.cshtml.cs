using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Emit;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide
{
    public class DashboardModel : BasePageModel
    {
        public DashboardModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }
        public List<string> DailyLabels { get; set; } = new();
        public List<decimal> DailyData { get; set; } = new();

        public List<string> MonthlyLabels { get; set; } = new();
        public List<decimal> MonthlyData { get; set; } = new();



        public async Task<IActionResult> OnGetAsync()
        {

            try
            {
                var dailyResponse = await _httpClient.GetStringAsync("owner/revenue-daily");
                Console.WriteLine($"Daily API Response: {dailyResponse}");

                if (!string.IsNullOrEmpty(dailyResponse))
                {
                    var dailyJson = JsonSerializer.Deserialize<JsonElement>(dailyResponse);
                    DailyLabels = dailyJson.GetProperty("labels").EnumerateArray().Select(x => x.GetString()).ToList();
                    DailyData = dailyJson.GetProperty("data").EnumerateArray().Select(x => x.GetDecimal()).ToList();
                }

                // 🟢 Fetch doanh thu hàng tháng
                var monthlyResponse = await _httpClient.GetStringAsync("owner/revenue-monthly");
                Console.WriteLine($"Monthly API Response: {monthlyResponse}");

                if (!string.IsNullOrEmpty(monthlyResponse))
                {
                    var monthlyJson = JsonSerializer.Deserialize<JsonElement>(monthlyResponse);
                    MonthlyLabels = monthlyJson.GetProperty("labels").EnumerateArray().Select(x => x.GetString()).ToList();
                    MonthlyData = monthlyJson.GetProperty("data").EnumerateArray().Select(x => x.GetDecimal()).ToList();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy dữ liệu API: {ex.Message}");
            }

            return Page();
        }

    }
}
