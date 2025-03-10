using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Staff
{
    public class EditModel : BasePageModel
    {
        public EditModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        [BindProperty]
        public StaffEditVM Staff { get; set; } = new StaffEditVM();

        [BindProperty]
        public List<ShopVM> Shops { get; set; } = new List<ShopVM>();
        [BindProperty]
        public List<RoleVM> Roles { get; set; } = new List<RoleVM>();
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }
            try
            {
                // Lấy thông tin Staff
                var response = await _httpClient.GetFromJsonAsync<StaffEditVM>($"owner/get-Staff/{id}");
                if (response == null)
                {
                    return NotFound();
                }
                Staff = response;

                // Lấy danh sách shop
                var shopResponse = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
                if (shopResponse != null)
                {
                    Shops = shopResponse;
                }
                // Lấy danh sách Role
                var roleResponse = await _httpClient.GetFromJsonAsync<List<RoleVM>>("owner/get-all-roles");
                if (roleResponse != null)
                {
                    Roles = roleResponse;
                }

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching Staff details.");
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }
            var avata = await _httpClient.GetFromJsonAsync<StaffEditVM>($"owner/get-Staff/{Request.Query["id"]}");
            if (avata != null)
            {
                Staff.AvatarUrl = avata.AvatarUrl;
            }
            // Lấy danh sách shop để hiển thị lại nếu có lỗi
            var shopResponse = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
            if (shopResponse != null)
            {
                Shops = shopResponse;
            }
            // Lấy danh sách Role
            var roleResponse = await _httpClient.GetFromJsonAsync<List<RoleVM>>("owner/get-all-roles");
            if (roleResponse != null)
            {
                Roles = roleResponse;
            }

            if (!ModelState.IsValid)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
                return Page();
            }

            using var content = new MultipartFormDataContent();
            // Thêm file Avatar vào request nếu có
            if (Staff.Avatar != null && Staff.Avatar.Length > 0)
            {
                var streamContent = new StreamContent(Staff.Avatar.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Staff.Avatar.ContentType);
                content.Add(streamContent, "Avatar", Staff.Avatar.FileName);
            }

            // Thêm các dữ liệu khác vào request
            content.Add(new StringContent(Staff.FirstName ?? ""), "FirstName");
            content.Add(new StringContent(Staff.LastName ?? ""), "LastName");
            content.Add(new StringContent(Staff.Email ?? ""), "Email");
            content.Add(new StringContent(Staff.PhoneNo ?? ""), "PhoneNo");
            content.Add(new StringContent(Staff.Sex ?? ""), "Sex");
            content.Add(new StringContent(Staff.ShopId.ToString()!), "ShopId");
            content.Add(new StringContent(Staff.RoleId.ToString()!), "RoleId");

            // Gửi request đến API
            var response = await _httpClient.PutAsync($"owner/update-staff/{Request.Query["id"]}", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateError());
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Cập nhật thất bại. " + errorMessage);
                return Page();
            }

            TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateSuccess());
            return RedirectToPage("Index");
        }

    }
}
