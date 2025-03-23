using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Services
{
    public interface IIngredientService
    {
        Task<bool> CreateIngredientAsync(Ingredient ingredient);
        Task<bool> UpdateIngredientAsync(int id, Ingredient ingredient);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<List<Ingredient>> GetAllIngredientsAsync();
    }
}
