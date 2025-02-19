using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Recipes = new HashSet<Recipe>();
        }

        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public int? Discount { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int IsActive { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
