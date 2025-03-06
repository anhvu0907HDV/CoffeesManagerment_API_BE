using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;


        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();

            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);

        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var product = await _productRepository.CreateProduct(productDto);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var product = await _productRepository.UpdateProduct(productDto);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }







    }
}
