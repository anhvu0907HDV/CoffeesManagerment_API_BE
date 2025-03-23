using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Manager
{
    public class AddModel : BasePageModel
    {
        public AddModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        [BindProperty]
        public ManagerAddVM Manager { get; set; } = new ManagerAddVM();
        [BindProperty]
        public List<ShopVM> Shops { get; set; } = new List<ShopVM>();
        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login"); // Xử lý redirect ở đây
            }
            try
            {
                // Lấy danh sách shop
                var response = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
                if (response != null)
                {
                    Shops = response;
                }
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching shops.");
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
            var shops = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
            if (shops != null)
            {
                Shops = shops;
            }
            TempData["Toast"] = JsonSerializer.Serialize(Toast.RegisterSuccess());

            var jsonContent = JsonSerializer.Serialize(Manager);

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
            content.Add(new StringContent(Manager.Password ?? ""), "Password");
            content.Add(new StringContent(Manager.ConfirmPassword ?? ""), "ConfirmPassword");
            content.Add(new StringContent(Manager.PhoneNo ?? ""), "PhoneNo");
            content.Add(new StringContent(Manager.Sex ?? ""), "Sex");
            content.Add(new StringContent(Manager.ShopId.ToString()), "ShopId");
            content.Add(new StringContent(Manager.AcceptTerms.ToString()), "AcceptTerms");

            // Gửi request đến API
            var response = await _httpClient.PostAsync("owner/create-manager", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.RegisterError());
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Đăng ký thất bại. " + errorMessage);
                return Page();
            }

            return RedirectToPage("Index");
        }

    }
}
