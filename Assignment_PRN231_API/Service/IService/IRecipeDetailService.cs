using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Services
{
    public interface IRecipeDetailService
    {
        Task<bool> AddIngredientsToRecipeAsync(RecipeDetail recipeDetail);
        Task<List<RecipeDetail>> GetIngredientsForRecipeAsync(int recipeId);
    }
}
