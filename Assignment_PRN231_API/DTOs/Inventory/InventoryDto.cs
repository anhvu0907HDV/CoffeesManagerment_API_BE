using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Inventory
{
    public class InventoryDto
    {
        [Required(ErrorMessage = "IngredientId is required.")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "ShopId is required.")]
        public int ShopId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "StockQuantity must be greater than or equal to 0.")]
        public decimal StockQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MinStockLevel must be greater than or equal to 0.")]
        public decimal MinStockLevel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MaxStockLevel must be greater than or equal to 0.")]
        public decimal MaxStockLevel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "PricePerUnit must be greater than or equal to 0.")]
        public decimal PricePerUnit { get; set; }
        public string? IngredientName { get; set; }
    }
}
