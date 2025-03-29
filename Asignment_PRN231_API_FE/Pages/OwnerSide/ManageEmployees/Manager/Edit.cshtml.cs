using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Manager
{
    [Authorize(Roles = "Owner")]
    public class EditModel : BasePageModel
    {
        public EditModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }
        [BindProperty]
        public ManagerEditVM Manager { get; set; } = new ManagerEditVM();

        [BindProperty]
        public List<ShopVM> Shops { get; set; } = new List<ShopVM>();
  

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login"); // Xử lý redirect ở đây
            }
            try
            {
                // Lấy thông tin Manager
                var response = await _httpClient.GetFromJsonAsync<ManagerEditVM>($"owner/get-manager/{id}");
                if (response == null)
                {
                    return NotFound();
                }
                Manager = response;

                // Lấy danh sách shop
                var shopResponse = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
                if (shopResponse != null)
                {
                    Shops = shopResponse;
                }

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching manager details.");
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login"); // Xử lý redirect ở đây
            }
            var avata = await _httpClient.GetFromJsonAsync<ManagerEditVM>($"owner/get-manager/{Request.Query["id"]}");
            if (avata != null)
            {
                Manager.AvatarUrl = avata.AvatarUrl;
            }
            // Lấy danh sách shop để hiển thị lại nếu có lỗi
            var shopResponse = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
            if (shopResponse != null)
            {
                Shops = shopResponse;
            }

            if (!ModelState.IsValid)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
                return Page();
            }

            using var content = new MultipartFormDataContent();
            // Thêm file Avatar vào request nếu có
            if (Manager.Avatar != null && Manager.Avatar.Length > 0)
            {
                var streamContent = new StreamContent(Manager.Avatar.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Manager.Avatar.ContentType);
                content.Add(streamContent, "Avatar", Manager.Avatar.FileName);
            }

            // Thêm các dữ liệu khác vào request
            content.Add(new StringContent(Manager.FirstName ?? ""), "FirstName");
            content.Add(new StringContent(Manager.LastName ?? ""), "LastName");
            content.Add(new StringContent(Manager.Email ?? ""), "Email");
            content.Add(new StringContent(Manager.PhoneNo ?? ""), "PhoneNo");
            content.Add(new StringContent(Manager.Sex ?? ""), "Sex");
            content.Add(new StringContent(Manager.ShopId.ToString()), "ShopId");

            // Gửi request đến API
            var response = await _httpClient.PutAsync($"owner/update-manager/{Manager.Id}", content);

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
