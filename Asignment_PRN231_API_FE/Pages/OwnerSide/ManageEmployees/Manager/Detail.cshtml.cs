using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Manager
{
    public class DetailModel : BasePageModel
    {
        public DetailModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
        : base(httpContextAccessor, authService, httpClientFactory)
        {
        }
        [BindProperty]
        public ManagerEditVM Manager { get; set; } = new ManagerEditVM();
        [BindProperty]
        public ShopVM ShopVM { get; set; } = new ShopVM();
        [BindProperty]
        public List<StaffVM> ListStaffs { get; set; } = new List<StaffVM>();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ManagerEditVM>($"owner/get-manager/{id}");
                if (response == null)
                {
                    return NotFound();
                }

                Manager = response;
                if (Manager.ShopId.HasValue)
                {
                    var shopResponse = await _httpClient.GetFromJsonAsync<ShopVM>($"shop/get-shop/{Manager.ShopId}");
                    if (shopResponse != null)
                    {
                        ShopVM = shopResponse;
                    }
                    // Lấy danh sách nhân viên trong shop của Manager
                    var staffResponse = await _httpClient.GetFromJsonAsync<List<StaffVM>>($"manager/staffs/{Manager.ShopId}");
                    if (staffResponse != null)
                    {
                        ListStaffs = staffResponse;
                    }
                }
                
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching manager details.");
                return Page();
            }
        }
    }
}
