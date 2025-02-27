using Asignment_PRN231_API_FE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class LogoutModel : PageModel
    {
        private readonly AuthService _authService;

        public LogoutModel(AuthService authService)
        {

            _authService = authService;
        }
        public IActionResult OnGet()
        {
            _authService.Logout();
            return RedirectToPage("./Login");  
        }
    }
}
