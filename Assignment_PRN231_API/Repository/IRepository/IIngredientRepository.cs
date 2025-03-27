using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IIngredientRepository
    {
        Task<Ingredient?> GetIngredientByIdAsync(int ingredientId);
        Task<Ingredient?> GetIngredientByNameAsync(string ingredientName);
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient> CreateIngredient(Ingredient ingredient);
        Task<Ingredient> UpdateIngredient(Ingredient ingredient);
        Task<bool> DeleteIngredient(int ingredientId);
    }
}
