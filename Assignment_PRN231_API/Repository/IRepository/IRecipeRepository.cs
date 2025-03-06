using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IRecipeRepository
    {
        Task<bool> CreateRecipeAsync(Recipe recipe);
        Task<bool> UpdateRecipeAsync(int id, Recipe recipe);
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetAllRecipesAsync();
    }
}
