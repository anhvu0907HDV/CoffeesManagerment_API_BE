using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.Ingredient
{
    [Authorize(Roles = "Owner")]
    public class IngredientsModel : BasePageModel
    {
        public IngredientsModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }

        public List<IngredientDto> Ingredients { get; set; } = new();
        [BindProperty] public IngredientDto NewIngredient { get; set; } = new();
        [BindProperty] public IngredientDto EditIngredient { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var response = await httpClient.GetAsync("ingredient/get-all-ingredients");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Ingredients = JsonSerializer.Deserialize<List<IngredientDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var json = JsonSerializer.Serialize(NewIngredient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("ingredient/create-ingredient", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateSuccess());
                return RedirectToPage();
            }
            TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateError());
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var json = JsonSerializer.Serialize(EditIngredient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"ingredient/update-ingredient/{EditIngredient.IngredientId}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateSuccess());

                return RedirectToPage();
            }
            TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateError());

            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var response = await httpClient.DeleteAsync($"ingredient/delete-ingredient/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.DeleteSuccess());
                return RedirectToPage();
            }
            TempData["Toast"] = JsonSerializer.Serialize(Toast.DeleteError());
            return RedirectToPage();

        }
    }

    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public decimal Unit { get; set; }
    }
}
