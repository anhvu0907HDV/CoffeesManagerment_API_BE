namespace Assignment_PRN231_API.DTOs.Recipe
{
    public class RecipeDetailDto
    {
        public int RecipeDetailId { get; set; }
        public decimal Quantity { get; set; }
        public string IngredientName { get; set; } = null!;
    }
}
