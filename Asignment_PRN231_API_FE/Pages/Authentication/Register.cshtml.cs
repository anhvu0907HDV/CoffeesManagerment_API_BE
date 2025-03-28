using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Dynamic;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");  
        }

        [BindProperty]
        public RegisterVM RegisterVM { get; set; } = new RegisterVM();
        public Toast toast { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var jsonContent = JsonSerializer.Serialize(RegisterVM);
            var shops = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
            RegisterVM.Shops = shops;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TempData["Toast"] = JsonSerializer.Serialize(Toast.RegisterSuccess());

            var jsonContent = JsonSerializer.Serialize(RegisterVM);
            var shops = await _httpClient.GetFromJsonAsync<List<ShopVM>>("shop/get-all-shops");
            RegisterVM.Shops = shops;

            if (!ModelState.IsValid)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
                return Page();
            }

            using var content = new MultipartFormDataContent();

            // Thêm file Avatar vào request nếu có
            if (RegisterVM.Avatar != null && RegisterVM.Avatar.Length > 0)
            {
                var streamContent = new StreamContent(RegisterVM.Avatar.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(RegisterVM.Avatar.ContentType);
                content.Add(streamContent, "Avatar", RegisterVM.Avatar.FileName);
            }

            // Thêm các dữ liệu khác vào request
            content.Add(new StringContent(RegisterVM.FirstName ?? ""), "FirstName");
            content.Add(new StringContent(RegisterVM.LastName ?? ""), "LastName");
            content.Add(new StringContent(RegisterVM.Email ?? ""), "Email");
            content.Add(new StringContent(RegisterVM.Password ?? ""), "Password");
            content.Add(new StringContent(RegisterVM.ConfirmPassword ?? ""), "ConfirmPassword");
            content.Add(new StringContent(RegisterVM.PhoneNo ?? ""), "PhoneNo");
            content.Add(new StringContent(RegisterVM.Sex ?? ""), "Sex");
            content.Add(new StringContent(RegisterVM.ShopId.ToString()), "ShopId");
            content.Add(new StringContent(RegisterVM.AcceptTerms.ToString()), "AcceptTerms");

            // Gửi request đến API
            var response = await _httpClient.PostAsync("api/account/register", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.RegisterError());
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Đăng ký thất bại. " + errorMessage);
                return Page();
            }

            return RedirectToPage("Login");
        }
    }
}
