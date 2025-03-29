using Assignment_PRN231_API.DTOs.Manager;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service;
using Assignment_PRN231_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("manager")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IIngredientService _ingredientService;
        private readonly IRecipeService _recipeService;
        private readonly IRecipeDetailService _recipeDetailService;
        public ManagerController(IManagerRepository managerRepository , IIngredientService ingredientService, IRecipeService recipeService, IRecipeDetailService recipeDetailService)
        {
            _managerRepository = managerRepository;
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

    }
}
