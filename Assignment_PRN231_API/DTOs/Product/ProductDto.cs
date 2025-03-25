
﻿using Asignment_PRN231_API.DTOs.AnotationCustom;
using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Owner
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Please select a product image.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? Image { get; set; }

        public int? RecipeId { get; set; }

        [Required(ErrorMessage = "The product category is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, ErrorMessage = "The product name must not exceed 100 characters.")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "The product description is required.")]
        [StringLength(500, ErrorMessage = "The description must not exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The product price must be greater than 0.")]
        public decimal Price { get; set; }

        public int? Discount { get; set; }

        [Required(ErrorMessage = "The size is required.")]
        public int Size { get; set; }

        public int Quantity { get; set; }
        public int IsActive { get; set; }
            
    }

}
