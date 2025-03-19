﻿using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.DTOs.Owner
{
    public class ProductDto
    {
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
            
    }
}
