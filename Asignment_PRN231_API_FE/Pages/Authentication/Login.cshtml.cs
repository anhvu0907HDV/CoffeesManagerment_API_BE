using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authService)
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
