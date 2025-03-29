using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asignment_PRN231_API_FE.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Authentication/Login");
            }

            if (User.IsInRole("Owner"))
            {
                return RedirectToPage("/OwnerSide/Dashboard");
            }
            else if (User.IsInRole("Manager"))
            {
                return RedirectToPage("/ManagerSide/Table/Index");
            }
            else if (User.IsInRole("Staff"))
            {
                return RedirectToPage("/StaffSide/ManageOrder/CreateOrder");
            }

            return Page();
        }
    }
}
