using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IIngredientRepository
    {
        Task<bool> CreateIngredientAsync(Ingredient ingredient);
        Task<bool> UpdateIngredientAsync(int id, Ingredient ingredient);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<bool> DeleteIngredientAsync(int id);
    }
}
