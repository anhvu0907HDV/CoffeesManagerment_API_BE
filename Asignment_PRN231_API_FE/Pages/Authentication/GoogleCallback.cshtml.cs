using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class GoogleCallbackModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var authResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authResult.Succeeded) return RedirectToPage("/Index");

            var claims = authResult.Principal.Identities.FirstOrDefault()?.Claims.Select(c => new { c.Type, c.Value });

            // Gửi token từ API về Razor Pages
            return RedirectToPage("/Profile", new { token = "your-token-here" });
        }
    }
}
