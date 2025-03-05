using api_VS.Data;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDBContext _context;

        public IngredientRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateIngredientAsync(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateIngredientAsync(int id, Ingredient ingredient)
        {
            var existingIngredient = await _context.Ingredients.FindAsync(id);
            if (existingIngredient == null) return false;

            existingIngredient.IngredientName = ingredient.IngredientName;
            existingIngredient.Unit = ingredient.Unit;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.IngredientId == id);
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }
    }
}
