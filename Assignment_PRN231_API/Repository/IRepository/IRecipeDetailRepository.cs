using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IRecipeDetailRepository
    {
        Task<bool> AddIngredientsToRecipeAsync(RecipeDetail recipeDetail);
        Task<List<RecipeDetail>> GetIngredientsForRecipeAsync(int recipeId);
    }
}
