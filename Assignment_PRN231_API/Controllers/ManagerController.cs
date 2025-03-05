using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service;
using Assignment_PRN231_API.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IProductService _productService;
        private readonly ITableService _tableService;
        public ManagerController(IManagerRepository managerRepository, IProductService productService, ITableService tableService)
        {
            _managerRepository = managerRepository;
            _productService = productService;
            _tableService = tableService;
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

        // Quản lý Sản phẩm (Product Management)

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null) return BadRequest("Product data is required.");
            var result = await _productService.CreateProductAsync(product);
            if (result)
                return CreatedAtAction(nameof(CreateProduct), new { id = product.ProductId }, product);
            return BadRequest("Error creating product.");
        }

        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null) return BadRequest("Product data is required.");
            var result = await _productService.UpdateProductAsync(id, product);
            if (result)
                return Ok("Product updated successfully.");
            return NotFound("Product not found.");
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
                return Ok(product);
            return NotFound("Product not found.");
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // Quản lý Bàn (Table Management)

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
    }
}
