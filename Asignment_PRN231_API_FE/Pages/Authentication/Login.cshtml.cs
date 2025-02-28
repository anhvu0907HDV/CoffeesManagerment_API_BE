using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Asignment_PRN231_API_FE.Pages.Common;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class LoginModel : BasePageModel
    {
        private readonly AuthService _authService;

        public LoginModel(IHttpContextAccessor httpContextAccessor, AuthService authService, HttpClient httpClient) : base(httpContextAccessor, authService, httpClient)
        {
            _authService = authService;
        }


        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public Toast toast { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (await _authService.IsAuthenticatedAsync())
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _authService.LoginAsync(Username, Password))
            {
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
