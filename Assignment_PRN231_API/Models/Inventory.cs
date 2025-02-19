using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Inventory
    {
        public int IngredientId { get; set; }
        public int ShopId { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal MinStockLevel { get; set; }
        public decimal MaxStockLevel { get; set; }
        public decimal PricePerUnit { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Shop Shop { get; set; } = null!;
    }
}
