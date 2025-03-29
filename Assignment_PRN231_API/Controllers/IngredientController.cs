using Assignment_PRN231_API.DTOs.Ingredient;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("ingredient")]
    [ApiController]
    [Authorize]

    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet("get-ingredient/{ingredientId}")]
        public async Task<IActionResult> GetIngredient(int ingredientId)
        {
            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(ingredientId);
            if (ingredient == null)
            {
                return NotFound(new { message = "Nguyên liệu không tồn tại!" });
            }

            var ingredientDto = _mapper.Map<IngredientDto>(ingredient);
            return Ok(ingredientDto);
        }

        [HttpGet("get-all-ingredients")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientRepository.GetAllIngredientsAsync();
            if (ingredients == null || !ingredients.Any())
            {
                return NotFound(new { message = "Không có nguyên liệu nào!" });
            }

            var ingredientDtos = _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
            return Ok(ingredientDtos);
        }

        [HttpPost("create-ingredient")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDto ingredientDto)
        {
            if (ingredientDto == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var existingIngredient = await _ingredientRepository.GetIngredientByNameAsync(ingredientDto.IngredientName);
            if (existingIngredient != null)
            {
                return BadRequest(new { message = "Nguyên liệu đã tồn tại!" });
            }

            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            var createdIngredient = await _ingredientRepository.CreateIngredient(ingredient);

            var resultDto = _mapper.Map<IngredientDto>(createdIngredient);
            return Ok(resultDto);
        }

        [HttpPut("update-ingredient/{id:int}")]
        public async Task<IActionResult> UpdateIngredient([FromRoute] int id, [FromBody] IngredientDto ingredientDto)
        {
            if (ingredientDto == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var existingIngredient = await _ingredientRepository.GetIngredientByIdAsync(id);
            if (existingIngredient == null)
            {
                return NotFound(new { message = "Nguyên liệu không tồn tại!" });
            }

            _mapper.Map(ingredientDto, existingIngredient);
            var updatedIngredient = await _ingredientRepository.UpdateIngredient(existingIngredient);

            var resultDto = _mapper.Map<IngredientDto>(updatedIngredient);
            return Ok(resultDto);
        }

        [HttpDelete("delete-ingredient/{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient([FromRoute] int ingredientId)
        {
            var result = await _ingredientRepository.DeleteIngredient(ingredientId);
            if (!result)
            {
                return NotFound(new { message = "Nguyên liệu không tồn tại!" });
            }

            return Ok(new { message = "Xóa nguyên liệu thành công!" });
        }
    }


}
