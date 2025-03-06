using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_PRN231_API.Services
{
    public class RecipeDetailService : IRecipeDetailService
    {
        private readonly IRecipeDetailRepository _recipeDetailRepository;

        public RecipeDetailService(IRecipeDetailRepository recipeDetailRepository)
        {
            _recipeDetailRepository = recipeDetailRepository;
        }

        public async Task<bool> AddIngredientsToRecipeAsync(RecipeDetail recipeDetail)
        {
            return await _recipeDetailRepository.AddIngredientsToRecipeAsync(recipeDetail);
        }

        public async Task<List<RecipeDetail>> GetIngredientsForRecipeAsync(int recipeId)
        {
            return await _recipeDetailRepository.GetIngredientsForRecipeAsync(recipeId);
        }
    }
}
