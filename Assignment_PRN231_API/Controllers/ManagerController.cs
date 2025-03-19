using Assignment_PRN231_API.DTOs.Manager;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service;
using Assignment_PRN231_API.Service.IService;
using Assignment_PRN231_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IProductRepository _productRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeDetailRepository _recipeDetailRepository;
        private readonly IMapper _mapper;
        public ManagerController(IMapper mapper,IManagerRepository managerRepository,IProductRepository productRepository  ,ITableRepository tableRepository, IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository, IRecipeDetailRepository recipeDetailRepository)
        {
            _managerRepository = managerRepository;         
            _tableRepository = tableRepository;
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _recipeDetailRepository = recipeDetailRepository;
            _mapper = mapper;
        }

        [HttpGet("staffs/{shopId:int}")]
        public async Task<IActionResult> GetAllStaffByShopId(int shopId)
        {
            
            var staffDtos = await _managerRepository.GetAllStaffByShopId(shopId);
            if (staffDtos == null)
            {
                return NotFound();
            }
            return Ok(staffDtos);
        }

        

        // Quản lý Bàn (Table Management)
        [HttpGet("tables")]
        public async Task<IActionResult> GetAllTables()
        {
            var tables = await _tableRepository.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("table/{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableRepository.GetTableByIdAsync(id);
            if (table != null)
                return Ok(table);
            return NotFound("Table not found.");
        }

        [HttpPost("create-table")]
        public async Task<IActionResult> CreateTable([FromForm] TableDto tableDto)
        {
            if (tableDto == null) return BadRequest("Table data is required.");
            var newTable = _mapper.Map<Table>(tableDto);

            var result = await _tableRepository.CreateTableAsync(newTable);
            if (result)
                return CreatedAtAction(nameof(CreateTable), new { id = newTable.TableId, message = "Create table successfully" });
            return BadRequest("Error creating table.");
        }

        [HttpDelete("delete-table/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableRepository.DeleteTableAsync(id);
            if (result)
                return NoContent(); // Successfully deleted
            return NotFound("Table not found.");
        }

        [HttpPut("update-table-status/{id}")]
        public async Task<IActionResult> UpdateTableStatus(int id, [FromForm] bool status)
        {
            var result = await _tableRepository.UpdateTableStatusAsync(id, status);
            if (result)
                return Ok("Table status updated.");
            return NotFound("Table not found.");
        }

        // Quản lý Nguyên liệu (Ingredient Management)
        [HttpPost("create-ingredient")]
        public async Task<IActionResult> CreateIngredient([FromForm] IngredientDto ingredientDto)
        {
            if (ingredientDto == null) return BadRequest("Ingredient data is required.");
            var newIngredient = _mapper.Map<Ingredient>(ingredientDto);
            var result = await _ingredientRepository.CreateIngredientAsync(newIngredient);
            if (result)
                return CreatedAtAction(nameof(CreateIngredient), new { id = newIngredient.IngredientId, message = "Create ingredient successfully" });
            return BadRequest("Error creating ingredient.");
        }

        [HttpPut("update-ingredient/{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromForm] IngredientDto ingredientDto)
        {
            if (ingredientDto == null) return BadRequest("Ingredient data is required.");
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            var result = await _ingredientRepository.UpdateIngredientAsync(id, ingredient);
            if (result)
                return Ok("Ingredient updated successfully.");
            return NotFound("Ingredient not found.");
        }

        [HttpGet("ingredient/{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(id);
            if (ingredient != null)
                return Ok(ingredient);
            return NotFound("Ingredient not found.");
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientRepository .GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        // Quản lý Công thức (Recipe Management)
        [HttpPost("create-recipe")]
        public async Task<IActionResult> CreateRecipe([FromForm] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Recipe data is required.");

            var product = await _productRepository.GetProductById(recipeDto.ProductId);
            if (product == null)
                return BadRequest($"Product with ID {recipeDto.ProductId} not found.");

            var recipe = _mapper.Map<Recipe>(recipeDto);
            var result = await _recipeRepository.CreateRecipeAsync(recipe);

            if (result)
                return CreatedAtAction(nameof(CreateRecipe), new { id = recipe.RecipeId }, recipe);

            return BadRequest("Error creating recipe.");
        }

        [HttpPut("update-recipe/{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromForm] RecipeDto recipeDto)
        {
            if (recipeDto == null) return BadRequest("Recipe data is required.");
            var recipe = _mapper.Map<Recipe>(recipeDto);
            var result = await _recipeRepository.UpdateRecipeAsync(id, recipe);
            if (result)
                return Ok("Recipe updated successfully.");
            return NotFound("Recipe not found.");
        }

        [HttpGet("recipe/{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            if (recipe != null)
                return Ok(recipe);
            return NotFound("Recipe not found.");
        }

        [HttpGet("recipes")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
            return Ok(recipes);
        }

        // Quản lý Chi tiết Công thức (Recipe Detail Management)
        [HttpPost("add-ingredient-to-recipe")]
        public async Task<IActionResult> AddIngredientToRecipe([FromForm] RecipeDetailDto recipeDetailDto)
        {
            if (recipeDetailDto == null) return BadRequest("RecipeDetail data is required.");
            var recipeDetail = _mapper.Map<RecipeDetail>(recipeDetailDto);
            var result = await _recipeDetailRepository.AddIngredientsToRecipeAsync(recipeDetail);
            if (result)
                return CreatedAtAction(nameof(AddIngredientToRecipe), new { id = recipeDetail.RecipeDetailId }, recipeDetail);
            return BadRequest("Error adding ingredient to recipe.");
        }

        [HttpGet("recipe/{recipeId}/ingredients")]
        public async Task<IActionResult> GetIngredientsForRecipe(int recipeId)
        {
            var ingredients = await _recipeDetailRepository.GetIngredientsForRecipeAsync(recipeId);
            if (ingredients != null && ingredients.Count > 0)
                return Ok(ingredients);
            return NotFound("No ingredients found for this recipe.");
        }
    }
}
