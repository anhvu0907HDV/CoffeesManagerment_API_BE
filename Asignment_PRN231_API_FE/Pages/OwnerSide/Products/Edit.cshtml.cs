using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.Products
{
    [Authorize(Roles = "Owner")]
    public class EditModel : BasePageModel
    {
        public EditModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
        : base(httpContextAccessor, authService, httpClientFactory) { }

        [BindProperty]
        public EditProductVM Product { get; set; } = new EditProductVM();

        [BindProperty]
        public List<CategoryVM> Categories { get; set; } = new List<CategoryVM>();
        [BindProperty]

        public List<RecipeDetailViewVM>? RecipeDetails { get; set; } = new List<RecipeDetailViewVM>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            try
            {
                var response = await _httpClient.GetAsync($"api/Product/get-product/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
                var responseBody = await response.Content.ReadAsStringAsync();

                Product = JsonSerializer.Deserialize<EditProductVM>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                Categories = await _httpClient.GetFromJsonAsync<List<CategoryVM>>("api/Product/all-category") ?? new List<CategoryVM>();

                if (Product.RecipeId != 0 && Product.RecipeId != null)
                {
                    response = await _httpClient.GetAsync($"api/Product/get-all-recipe-detail/{Product.RecipeId}");
                    if (!response.IsSuccessStatusCode)
                    {
                        RecipeDetails = new List<RecipeDetailViewVM>();
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        RecipeDetails = JsonSerializer.Deserialize<List<RecipeDetailViewVM>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                    }
                }

                return Page();
            }
            catch
            {
                ModelState.AddModelError("", "Không thể tải sản phẩm.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("ImageURL");
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            Categories = await _httpClient.GetFromJsonAsync<List<CategoryVM>>("api/Product/all-category") ?? new List<CategoryVM>();

            if (Product.RecipeId != 0 && Product.RecipeId != null)
            {
                var responseRecipe = await _httpClient.GetAsync($"api/Product/get-all-recipe-detail/{Product.RecipeId}");

                if (!responseRecipe.IsSuccessStatusCode)
                {
                    RecipeDetails = new List<RecipeDetailViewVM>();
                }
                else
                {
                    var result = await responseRecipe.Content.ReadAsStringAsync();
                    RecipeDetails = JsonSerializer.Deserialize<List<RecipeDetailViewVM>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }

            }

            if (!ModelState.IsValid)
            {
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
            content.Add(new StringContent(Product.ProductId.ToString()), "ProductId");
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

            var response = await _httpClient.PutAsync($"api/Product/update-product/{Product.ProductId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Cập nhật sản phẩm thất bại. " + errorMessage);
                TempData["Toast"] = JsonSerializer.Serialize(Toast.CreateError());
                return Page();
            }

            TempData["Toast"] = JsonSerializer.Serialize(Toast.UpdateSuccess());
            return RedirectToPage("Index");
        }
    }
}
