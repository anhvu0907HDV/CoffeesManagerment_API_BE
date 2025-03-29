using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asignment_PRN231_API_FE.Pages.StaffSide.ManageOrder
{
    public class ViewRecipeModel : BasePageModel
    {
        public ViewRecipeModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }

        public RecipeVM Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int productId)
        {
            var client = await GetAuthorizedHttpClientAsync();
            if (client == null) return RedirectToPage("/Authentication/Login");

            var response = await client.GetAsync($"https://localhost:7079/staff/GetRecipeByProductId/{productId}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Không tìm thấy công thức.";
                return RedirectToPage("/StaffSide/ManageOrder/ViewOrder");
            }

            Recipe = await response.Content.ReadFromJsonAsync<RecipeVM>();
            return Page();
        }

        public class RecipeVM
        {
            public int RecipeId { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public List<RecipeDetailVM> RecipeDetails { get; set; } = new();
        }

        public class RecipeDetailVM
        {
            public int RecipeDetailId { get; set; }
            public double Quantity { get; set; }
            public string IngredientName { get; set; }
        }
    }
}
