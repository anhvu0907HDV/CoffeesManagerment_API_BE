using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Asignment_PRN231_API_FE.Pages.Common;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.Pages.Authentication
{
    public class LoginModel : BasePageModel
    {
        private readonly AuthService _authService;

        public LoginModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) 
            : base(httpContextAccessor, authService, httpClientFactory)
        {
            _authService = authService;
        }


        [BindProperty]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
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
            var role = await _authService.LoginAsync(Username, Password);
            if (role != null)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Success());

                if (role.Contains("Owner"))
                    return RedirectToPage("/OwnerSide/Dashboard");

                if (role.Contains("Manager"))
                {
                    var shopIdFromSession = _httpContextAccessor.HttpContext.Session.GetString("ShopId");
                    if (int.TryParse(shopIdFromSession, out int shopId))
                    {
                    return RedirectToPage("/ManagerSide/Table/Index", new { shopId });
                    }
                }

                if (role.Contains("Staff"))
                    return RedirectToPage("/index");

                if (role.Contains("unknow"))
                {
                    TempData["Toast"] = JsonSerializer.Serialize(Toast.Warning());
                    return Page();
                }
            }

            TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
            ErrorMessage = "Invalid username or password.";
            return Page();
        }


    }
}
