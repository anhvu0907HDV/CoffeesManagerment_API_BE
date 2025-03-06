using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service;
using Assignment_PRN231_API.Service.IService;
using Assignment_PRN231_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly ITableService _tableService;
        private readonly IIngredientService _ingredientService;
        private readonly IRecipeService _recipeService;
        private readonly IRecipeDetailService _recipeDetailService;
        public ManagerController(IManagerRepository managerRepository, ITableService tableService, IIngredientService ingredientService, IRecipeService recipeService, IRecipeDetailService recipeDetailService)
        {
            _managerRepository = managerRepository;
            _tableService = tableService;
            _ingredientService = ingredientService;
            _recipeService = recipeService;
            _recipeDetailService = recipeDetailService;
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

        [HttpPost("create-table")]
        public async Task<IActionResult> CreateTable([FromBody] Table table)
        {
            if (table == null) return BadRequest("Table data is required.");
            var result = await _tableService.CreateTableAsync(table);
            if (result)
                return CreatedAtAction(nameof(CreateTable), new { id = table.TableId }, table);
            return BadRequest("Error creating table.");
        }

        [HttpDelete("delete-table/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (result)
                return NoContent(); // Successfully deleted
            return NotFound("Table not found.");
        }

        [HttpPut("update-table-status/{id}")]
        public async Task<IActionResult> UpdateTableStatus(int id, [FromBody] bool status)
        {
            var result = await _tableService.UpdateTableStatusAsync(id, status);
            if (result)
                return Ok("Table status updated.");
            return NotFound("Table not found.");
        }

        [HttpGet("table/{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table != null)
                return Ok(table);
            return NotFound("Table not found.");
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        // Quản lý Nguyên liệu (Ingredient Management)

        [HttpPost("create-ingredient")]
        public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
        {
            if (ingredient == null) return BadRequest("Ingredient data is required.");
            var result = await _ingredientService.CreateIngredientAsync(ingredient);
            if (result)
                return CreatedAtAction(nameof(CreateIngredient), new { id = ingredient.IngredientId }, ingredient);
            return BadRequest("Error creating ingredient.");
        }

        [HttpPut("update-ingredient/{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] Ingredient ingredient)
        {
            if (ingredient == null) return BadRequest("Ingredient data is required.");
            var result = await _ingredientService.UpdateIngredientAsync(id, ingredient);
            if (result)
                return Ok("Ingredient updated successfully.");
            return NotFound("Ingredient not found.");
        }

        [HttpGet("ingredient/{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient != null)
                return Ok(ingredient);
            return NotFound("Ingredient not found.");
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        // Quản lý Công thức (Recipe Management)

        [HttpPost("create-recipe")]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null) return BadRequest("Recipe data is required.");
            var result = await _recipeService.CreateRecipeAsync(recipe);
            if (result)
                return CreatedAtAction(nameof(CreateRecipe), new { id = recipe.RecipeId }, recipe);
            return BadRequest("Error creating recipe.");
        }

        [HttpPut("update-recipe/{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipe recipe)
        {
            if (recipe == null) return BadRequest("Recipe data is required.");
            var result = await _recipeService.UpdateRecipeAsync(id, recipe);
            if (result)
                return Ok("Recipe updated successfully.");
            return NotFound("Recipe not found.");
        }

        [HttpGet("recipe/{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe != null)
                return Ok(recipe);
            return NotFound("Recipe not found.");
        }

        [HttpGet("recipes")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        // Quản lý Chi tiết Công thức (Recipe Detail Management)

        [HttpPost("add-ingredient-to-recipe")]
        public async Task<IActionResult> AddIngredientToRecipe([FromBody] RecipeDetail recipeDetail)
        {
            if (recipeDetail == null) return BadRequest("RecipeDetail data is required.");
            var result = await _recipeDetailService.AddIngredientsToRecipeAsync(recipeDetail);
            if (result)
                return CreatedAtAction(nameof(AddIngredientToRecipe), new { id = recipeDetail.RecipeDetailId }, recipeDetail);
            return BadRequest("Error adding ingredient to recipe.");
        }

        [HttpGet("recipe/{recipeId}/ingredients")]
        public async Task<IActionResult> GetIngredientsForRecipe(int recipeId)
        {
            var ingredients = await _recipeDetailService.GetIngredientsForRecipeAsync(recipeId);
            if (ingredients != null && ingredients.Count > 0)
                return Ok(ingredients);
            return NotFound("No ingredients found for this recipe.");
        }
    }
}
