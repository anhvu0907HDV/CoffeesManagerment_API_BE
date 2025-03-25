// File: Pages/OwnerSide/ManageIngredients/IngredientManagement.cshtml.cs

using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageIngredients
{
    public class IngredientManagementModel : BasePageModel
    {
        public IngredientManagementModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }
        public List<IngredientVM> Ingredients { get; set; } = new();

        [BindProperty]
        public IngredientVM Ingredient { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? EditId { get; set; }

        public bool ShowFormModal { get; set; } = false;


        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("manager/ingredients");
            if (response.IsSuccessStatusCode)
            {
                Ingredients = await response.Content.ReadFromJsonAsync<List<IngredientVM>>();
            }

            if (EditId.HasValue)
            {
                var editRes = await _httpClient.GetAsync($"manager/ingredient/{EditId.Value}");
                if (editRes.IsSuccessStatusCode)
                {
                    Ingredient = await editRes.Content.ReadFromJsonAsync<IngredientVM>();
                    ShowFormModal = true;
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ShowFormModal = true;
                return Page();
            }

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(Ingredient.Name), "Name");
            formData.Add(new StringContent(Ingredient.Quantity.ToString()), "Quantity");
            formData.Add(new StringContent(Ingredient.Unit.ToString()), "Unit");
            formData.Add(new StringContent(Ingredient.Status.ToString()), "Status");

            HttpResponseMessage response;
            if (Ingredient.Id > 0)
            {
                response = await _httpClient.PutAsync($"manager/update-ingredient/{Ingredient.Id}", formData);
            }
            else
            {
                response = await _httpClient.PostAsync("manager/create-ingredient", formData);
            }

            if (!response.IsSuccessStatusCode)
            {
                TempData["Toast"] = "Failed to save ingredient.";
                ShowFormModal = true;
                return Page();
            }

            TempData["Toast"] = "Ingredient saved successfully.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"manager/delete-ingredient/{id}");
            TempData["Toast"] = response.IsSuccessStatusCode ? "Ingredient deleted." : "Failed to delete.";
            return RedirectToPage();
        }
    }
}
