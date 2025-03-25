using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_PRN231_API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<bool> CreateIngredientAsync(Ingredient ingredient)
        {
            return await _ingredientRepository.CreateIngredientAsync(ingredient);
        }

        public async Task<bool> UpdateIngredientAsync(int id, Ingredient ingredient)
        {
            return await _ingredientRepository.UpdateIngredientAsync(id, ingredient);
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _ingredientRepository.GetIngredientByIdAsync(id);
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _ingredientRepository.GetAllIngredientsAsync();
        }
        public Task<bool> DeleteIngredientAsync(int id)
        => _ingredientRepository.DeleteIngredientAsync(id);
    }
}
