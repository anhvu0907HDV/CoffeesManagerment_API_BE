using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API"); // Định nghĩa "API" trong `Program.cs`
        }

        [BindProperty]
        public RegisterVM RegisterVM { get; set; } = new RegisterVM();
        public Toast toast { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var jsonContent = JsonSerializer.Serialize(RegisterVM);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/register", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync(); // Đọc nội dung phản hồi từ API
                toast = Toast.Error();
                ModelState.AddModelError("", "Đăng ký thất bại. ");
                return Page();
            }
            toast = Toast.Success();
            return RedirectToPage("Login"); 
        }
    }
}
