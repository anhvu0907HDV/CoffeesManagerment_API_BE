using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.Products
{
    [Authorize(Roles = "Owner")]
    public class CreateModel : BasePageModel
    {
        public CreateModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
        : base(httpContextAccessor, authService, httpClientFactory) { }

        [BindProperty]
        public AddProductVM Product { get; set; } = new AddProductVM(); 
        [BindProperty]
        public List<RecipeDetailsDto> RecipeDetails { get; set; } = new List<RecipeDetailsDto>();

        [BindProperty]
        public List<CategoryVM> Categories { get; set; } = new List<CategoryVM>(); 
        [BindProperty]
        public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            try
            {
                Categories = await _httpClient.GetFromJsonAsync<List<CategoryVM>>("api/Product/all-category") ?? new List<CategoryVM>();
                Ingredients = await _httpClient.GetFromJsonAsync<List<IngredientDto>>("api/Product/all-ingredient") ?? new List<IngredientDto>();
                return Page();
            }
            catch
            {
                ModelState.AddModelError("", "Không thể tải danh mục sản phẩm.");
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

            Categories = await _httpClient.GetFromJsonAsync<List<CategoryVM>>("api/Product/all-category") ?? new List<CategoryVM>();
            Ingredients = await _httpClient.GetFromJsonAsync<List<IngredientDto>>("api/Product/all-ingredient") ?? new List<IngredientDto>();


            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"🚨 Lỗi ModelState: {entry.Key} - {error.ErrorMessage}");
                    }
                }
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
                return Page();
            }

            using var content = new MultipartFormDataContent();

            if (Product.Image != null && Product.Image.Length > 0)
            {
                var streamContent = new StreamContent(Product.Image.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Product.Image.ContentType);
                content.Add(streamContent, "Image", Product.Image.FileName);
            }

            content.Add(new StringContent(Product.ProductName), "ProductName");
            content.Add(new StringContent(Product.Description ?? ""), "Description");
            content.Add(new StringContent(Product.Price.ToString()), "Price");
            content.Add(new StringContent(Product.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(Product.Size.ToString()), "Size");
            content.Add(new StringContent(Product.Quantity.ToString()), "Quantity");
            content.Add(new StringContent(Product.IsActive.ToString()), "IsActive");

            if (Product.Discount.HasValue)
            {
                content.Add(new StringContent(Product.Discount.Value.ToString()), "Discount");
            }

            var response = await _httpClient.PostAsync("api/Product/create-product", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Thêm sản phẩm thất bại. " + errorMessage);
                TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateError());
                return Page();
            }


            var json = JsonSerializer.Serialize(RecipeDetails);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CreateProductResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result != null)
            {
                int recipeId = result.RecipeId;
                var contentRcipe = new StringContent(json, Encoding.UTF8, "application/json");

                response = await _httpClient.PostAsync($"api/Product/create-recipe-detail?recipeId={recipeId}", contentRcipe);

            }
           
            TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateSuccess());
            return RedirectToPage("Index");
        }
        
    }
    public class CreateProductResponse
    {
        public int RecipeId { get; set; }
        public string Message { get; set; }
    }
}
