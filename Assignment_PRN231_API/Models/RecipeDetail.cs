using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class RecipeDetail
    {
        public int RecipeDetailId { get; set; }
        public int RecipeId { get; set; }
        public decimal Quantity { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
