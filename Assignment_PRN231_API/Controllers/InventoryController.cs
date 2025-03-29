using Assignment_PRN231_API.DTOs.Inventory;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("inventory")]
    [ApiController]
    [Authorize]

    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public InventoryController(
            IInventoryRepository inventoryRepository,
            IShopRepository shopRepository,
            IIngredientRepository ingredientRepository,
            IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _shopRepository = shopRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet("get-inventory/{shopId}")]
        public async Task<IActionResult> GetInventory(int shopId)
        {
            var inventoryItems = await _inventoryRepository.GetAllInventoryItems(shopId);
            if (inventoryItems == null || !inventoryItems.Any())
                return NotFound(new { message = "Không tìm thấy nguyên liệu trong kho!" });

            return Ok(_mapper.Map<List<InventoryDto>>(inventoryItems));
        }

        [HttpGet("get-inventory-item/{ingredientId}/{shopId}")]
        public async Task<IActionResult> GetInventoryItem(int ingredientId, int shopId)
        {
            var inventoryItem = await _inventoryRepository.GetInventoryItemById(ingredientId, shopId);
            if (inventoryItem == null)
                return NotFound(new { message = "Nguyên liệu không tồn tại trong kho!" });

            return Ok(_mapper.Map<InventoryDto>(inventoryItem));
        }

        [HttpPost("create-inventory-item")]
        public async Task<IActionResult> CreateInventoryItem([FromBody] InventoryDto inventoryDto)
        {
            if (inventoryDto == null)
                return BadRequest(new { message = "Dữ liệu nguyên liệu không hợp lệ!" });

            var shop = await _shopRepository.GetShopById(inventoryDto.ShopId);
            if (shop == null)
                return NotFound(new { message = "Cửa hàng không tồn tại!" });

            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(inventoryDto.IngredientId);
            if (ingredient == null)
                return NotFound(new { message = "Nguyên liệu không tồn tại!" });

            var existingInventory = await _inventoryRepository.GetInventoryItemById(inventoryDto.IngredientId, inventoryDto.ShopId);
            if (existingInventory != null)
                return BadRequest(new { message = "Nguyên liệu này đã tồn tại trong kho!" });

            var inventoryEntity = _mapper.Map<Inventory>(inventoryDto);
            var createdInventory = await _inventoryRepository.CreateInventoryItem(inventoryEntity);

            return CreatedAtAction(nameof(GetInventoryItem),
                new { ingredientId = createdInventory.IngredientId, shopId = createdInventory.ShopId },
                _mapper.Map<InventoryDto>(createdInventory));
        }

        [HttpPut("update-inventory-item/{ingredientId}/{shopId}")]
        public async Task<IActionResult> UpdateInventoryItem(int ingredientId, int shopId, [FromBody] InventoryDto inventoryDto)
        {
            if (inventoryDto == null)
                return BadRequest(new { message = "Dữ liệu nguyên liệu không hợp lệ!" });

            var existingInventory = await _inventoryRepository.GetInventoryItemById(ingredientId, shopId);
            if (existingInventory == null)
                return NotFound(new { message = "Không tìm thấy nguyên liệu trong kho!" });

            _mapper.Map(inventoryDto, existingInventory);
            var updatedInventory = await _inventoryRepository.UpdateInventoryItem(existingInventory);

            return Ok(_mapper.Map<InventoryDto>(updatedInventory));
        }

        [HttpDelete("delete-inventory-item/{ingredientId}/{shopId}")]
        public async Task<IActionResult> DeleteInventoryItem(int ingredientId, int shopId)
        {
            var existingInventory = await _inventoryRepository.GetInventoryItemById(ingredientId, shopId);
            if (existingInventory == null)
                return NotFound(new { message = "Không tìm thấy nguyên liệu trong kho!" });

            var result = await _inventoryRepository.DeleteInventoryItem(ingredientId, shopId);
            if (!result)
                return BadRequest(new { message = "Không thể xóa nguyên liệu khỏi kho!" });

            return Ok(new { message = "Xóa nguyên liệu khỏi kho thành công!" });
        }
    }


}
