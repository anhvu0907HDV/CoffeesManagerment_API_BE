using api_VS.Data;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class RecipeDetailRepository : IRecipeDetailRepository
    {
        private readonly ApplicationDBContext _context;

        public RecipeDetailRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        // Add ingredients to a recipe
        public async Task<bool> AddIngredientsToRecipeAsync(RecipeDetail recipeDetail)
        {
            await _context.RecipeDetails.AddAsync(recipeDetail);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;  // If changes were made, return true
        }

        // Get all ingredients for a specific recipe
        public async Task<List<RecipeDetail>> GetIngredientsForRecipeAsync(int recipeId)
        {
            return await _context.RecipeDetails
                                 .Where(rd => rd.RecipeId == recipeId)
                                 .Include(rd => rd.Ingredient)  // Include Ingredient details if necessary
                                 .ToListAsync();
        }
    }
}
