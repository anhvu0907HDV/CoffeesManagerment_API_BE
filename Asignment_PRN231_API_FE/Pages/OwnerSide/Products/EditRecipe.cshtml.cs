using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.Products
{
    public class EditRecipeModel : BasePageModel
    {
        public EditRecipeModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, authService, httpClientFactory)
        {
        }

        [BindProperty]
        public List<RecipeDetailsDto> RecipeDetails { get; set; } = new List<RecipeDetailsDto>();
        [BindProperty]
        public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
        [BindProperty]
        public int RecipeId { get; set; }
        [BindProperty]
        public int ProductId { get; set; }

        public async Task<IActionResult> OnGetAsync(int productId, int recipeId)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            try
            {
                RecipeId = recipeId;
                // Lấy danh sách chi tiết công thức
                var response = await _httpClient.GetAsync($"api/Product/get-all-recipe-detail/{recipeId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    RecipeDetails = JsonSerializer.Deserialize<List<RecipeDetailsDto>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<RecipeDetailsDto>();
                }
                else
                {
                    ModelState.AddModelError("", "Không thể tải chi tiết công thức.");
                }

                // Lấy danh sách nguyên liệu
                var ingredientResponse = await _httpClient.GetAsync("api/Product/all-ingredient");
                if (ingredientResponse.IsSuccessStatusCode)
                {
                    var ingredientBody = await ingredientResponse.Content.ReadAsStringAsync();
                    Ingredients = JsonSerializer.Deserialize<List<IngredientDto>>(ingredientBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<IngredientDto>();
                }
                else
                {
                    ModelState.AddModelError("", "Không thể tải danh sách nguyên liệu.");
                }

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi tải dữ liệu: {ex.Message}");
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
                return Page();
            }

            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(RecipeDetails),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"api/Product/create-or-update-recipe-details?recipeId={RecipeId}", jsonContent);

                // Lấy danh sách nguyên liệu
                var ingredientResponse = await _httpClient.GetAsync("api/Product/all-ingredient");
                if (ingredientResponse.IsSuccessStatusCode)
                {
                    var ingredientBody = await ingredientResponse.Content.ReadAsStringAsync();
                    Ingredients = JsonSerializer.Deserialize<List<IngredientDto>>(ingredientBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<IngredientDto>();
                }
                else
                {
                    ModelState.AddModelError("", "Không thể tải danh sách nguyên liệu.");
                }

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Cập nhật công thức thất bại. " + errorMessage);
                    TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateError());
                    return Page();
                }

                TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateSuccess());
                // Điều hướng về trang sản phẩm với productId
                return RedirectToPage("Edit", new { id = ProductId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật công thức: " + ex.Message);
                return Page();
            }
        }

    }

}
