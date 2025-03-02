using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        [HttpGet("get-shop/{shopId}")]
        public async Task<IActionResult> GetShop(int shopId)
        {
            var shop = await _shopRepository.GetShopById(shopId);

            if (shop == null)
            {
                return NotFound();
            }
            return Ok(shop);
        }
        [HttpGet("get-all-shops")]
        public async Task<IActionResult> GetShops()
        {
            var shops =await _shopRepository.GetAllShops();

            if (shops == null)
            {
                return NotFound();
            }
            return Ok(shops);
        }
        [HttpPost("create-shop")]
        public async Task<IActionResult> CreateShop([FromBody] ShopDto shopDto)
        {
            var shop = await _shopRepository.CreateShop(shopDto);
            if (shop == null) {
                return BadRequest();
            }
            return Ok(shop);
        }
        [HttpPut("update-shop")]
        public async Task<IActionResult> UpdateShop([FromBody] ShopDto shopDto)
        {
            var shop = await _shopRepository.UpdateShop(shopDto);
            if (shop == null)
            {
                return BadRequest();
            }
            return Ok(shop);
        }
        [HttpDelete("delete-shop/{shopId}")]
        public async Task<IActionResult> DeleteShop(int shopId)
        {
            var shop = await _shopRepository.DeleteShop(shopId);
            if (shop == null)
            {
                return BadRequest();
            }
            return Ok(shop);
        }

    }
}
