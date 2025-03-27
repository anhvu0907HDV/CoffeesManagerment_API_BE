using api_VS.Data;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDBContext _context;

        public RecipeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        // Implementing CreateRecipeAsync as defined in IRecipeRepository
        public async Task<bool> CreateRecipeAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        // Implementing UpdateRecipeAsync as defined in IRecipeRepository
        public async Task<bool> UpdateRecipeAsync(int id, Recipe recipe)
        {
            var existingRecipe = await _context.Recipes.FindAsync(id);
            if (existingRecipe != null)
            {
           
                existingRecipe.Description = recipe.Description;
                // Update other fields as necessary
                _context.Recipes.Update(existingRecipe);
                int changes = await _context.SaveChangesAsync();
                return changes > 0;
            }
            return false;
        }

        // Get all recipes
        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        // Get a recipe by its ID
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .FirstOrDefaultAsync(r => r.RecipeId == id);
        }

        // Delete a recipe
        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                int changes = await _context.SaveChangesAsync();
                return changes > 0;
            }
            return false;
        }
    }
}
