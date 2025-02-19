using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RecipeDetails = new HashSet<RecipeDetail>();
        }

        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public decimal Unit { get; set; }

        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
