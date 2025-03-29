using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Asignment_PRN231_API_FE.Pages.ManagerSide.Inventory
{
    [Authorize(Roles = "Manager")]
    public class InventoryModel : BasePageModel
    {
        public InventoryModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }

        [BindProperty(SupportsGet = true)]
        public int ShopId { get; set; }

        public List<InventoryDto> InventoryItems { get; set; } = new();
        public List<IngredientDto> Ingredients { get; set; } = new();

        [BindProperty] public InventoryDto NewInventoryItem { get; set; } = new();
        [BindProperty] public InventoryDto EditInventoryItem { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            // Lấy danh sách nguyên liệu từ API
            var ingredientResponse = await httpClient.GetAsync("ingredient/get-all-ingredients");
            if (ingredientResponse.IsSuccessStatusCode)
            {
                var json = await ingredientResponse.Content.ReadAsStringAsync();
                Ingredients = JsonSerializer.Deserialize<List<IngredientDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // Lấy danh sách nguyên liệu trong kho của cửa hàng
            var inventoryResponse = await httpClient.GetAsync($"inventory/get-inventory/{ShopId}");
            if (inventoryResponse.IsSuccessStatusCode)
            {
                var json = await inventoryResponse.Content.ReadAsStringAsync();
                InventoryItems = JsonSerializer.Deserialize<List<InventoryDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            var json = JsonSerializer.Serialize(NewInventoryItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("inventory/create-inventory-item", content);
            TempData["Toast"] = response.IsSuccessStatusCode
                ? JsonSerializer.Serialize(Toast.CreateSuccess())
                : JsonSerializer.Serialize(Toast.CreateError());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            var json = JsonSerializer.Serialize(new
            {
                EditInventoryItem.IngredientId,
                EditInventoryItem.ShopId,
                EditInventoryItem.IngredientName,
                EditInventoryItem.StockQuantity,
                EditInventoryItem.MinStockLevel,
                EditInventoryItem.MaxStockLevel,
                EditInventoryItem.PricePerUnit
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"inventory/update-inventory-item/{EditInventoryItem.IngredientId}/{ShopId}", content);
            TempData["Toast"] = response.IsSuccessStatusCode
                ? JsonSerializer.Serialize(Toast.UpdateSuccess())
                : JsonSerializer.Serialize(Toast.UpdateError());

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int ingredientId)
        {
            var httpClient = await GetAuthorizedHttpClientAsync();
            if (httpClient == null) return RedirectToPage("/Authentication/Login");

            var response = await httpClient.DeleteAsync($"inventory/delete-inventory-item/{ingredientId}/{ShopId}");
            TempData["Toast"] = response.IsSuccessStatusCode
                ? JsonSerializer.Serialize(Toast.DeleteSuccess())
                : JsonSerializer.Serialize(Toast.DeleteError());

            return RedirectToPage();
        }
    }

    public class InventoryDto
    {
        public int IngredientId { get; set; }
        public int ShopId { get; set; }
        public string IngredientName { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "StockQuantity must be greater than or equal to 0.")]
        public decimal StockQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MinStockLevel must be greater than or equal to 0.")]
        public decimal MinStockLevel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MaxStockLevel must be greater than or equal to 0.")]
        public decimal MaxStockLevel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "PricePerUnit must be greater than or equal to 0.")]
        public decimal PricePerUnit { get; set; }
    }

}
