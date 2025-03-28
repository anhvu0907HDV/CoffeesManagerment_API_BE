namespace Assignment_PRN231_API.DTOs.Ingredient
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public decimal Unit { get; set; }
    }
}
