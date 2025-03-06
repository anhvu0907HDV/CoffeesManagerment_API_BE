using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_PRN231_API.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<bool> CreateRecipeAsync(Recipe recipe)
        {
            return await _recipeRepository.CreateRecipeAsync(recipe);
        }

        public async Task<bool> UpdateRecipeAsync(int id, Recipe recipe)
        {
            return await _recipeRepository.UpdateRecipeAsync(id, recipe);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetRecipeByIdAsync(id);
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllRecipesAsync();
        }
    }
}
