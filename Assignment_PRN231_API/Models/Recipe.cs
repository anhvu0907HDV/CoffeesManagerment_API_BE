using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeDetails = new HashSet<RecipeDetail>();
        }

        public int RecipeId { get; set; }
        public int ProductId { get; set; }
        public string? Description { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
