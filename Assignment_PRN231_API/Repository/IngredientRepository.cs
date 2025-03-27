using api_VS.Data;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Google;
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

        // 🔹 Lấy nguyên liệu theo ID
        public async Task<Ingredient?> GetIngredientByIdAsync(int ingredientId)
        {
            return await _context.Ingredients.FindAsync(ingredientId);
        }

        // 🔹 Lấy nguyên liệu theo tên
        public async Task<Ingredient?> GetIngredientByNameAsync(string ingredientName)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.IngredientName.ToLower() == ingredientName.ToLower());
        }

        // 🔹 Lấy tất cả nguyên liệu
        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // 🔹 Tạo nguyên liệu mới
        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        // 🔹 Cập nhật nguyên liệu
        public async Task<Ingredient> UpdateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        // 🔹 Xóa nguyên liệu
        public async Task<bool> DeleteIngredient(int ingredientId)
        {
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient == null) return false;

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
