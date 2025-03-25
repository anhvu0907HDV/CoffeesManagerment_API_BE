namespace Assignment_PRN231_API.DTOs.Recipe
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public List<RecipeDetailDto> RecipeDetails { get; set; } = new List<RecipeDetailDto>();
    }
}
