using api_VS.Data;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Product;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper, ApplicationDBContext context)
        {
            _productRepository = productRepository;
            _context = context;
            _mapper = mapper;
            
        }
        [HttpPut("update-product/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int productId, [FromForm] ProductEditDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingProduct = await _productRepository.GetProductById(productId);
                if (existingProduct == null)
                {
                    return NotFound(new { Message = "Product not found." });
                }
                var oldImagePath = existingProduct.Image;
                // Cập nhật thông tin từ DTO
                _mapper.Map(productDto, existingProduct);

                // Xử lý upload ảnh mới nếu có
                if (productDto.Image != null && productDto.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/products");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDto.Image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.Image.CopyToAsync(stream);
                    }

                    // Xóa ảnh cũ (nếu có)
                    if (!string.IsNullOrEmpty(existingProduct.Image))
                    {
                        oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    existingProduct.Image = $"uploads/products/{fileName}";
                }
                else
                {
                    existingProduct.Image = oldImagePath; // Giữ lại ảnh cũ nếu không có ảnh mới
                }

                var updatedProduct = await _productRepository.UpdateProduct(existingProduct);

                return Ok(new
                {
                    ProductId = updatedProduct.ProductId,
                    ProductName = updatedProduct.ProductName,
                    ImageUrl = updatedProduct.Image,
                    RecipeId = updatedProduct.RecipeId,
                    Message = "Product updated successfully!"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = e.Message });
            }
        }

        [HttpGet("get-all-recipe-detail/{recipeId}")]
        public async Task<IActionResult> GetAllRecipeDetails(int recipeId)
        {
            try
            {
                var recipeDetails = await _productRepository.GetRecipeDetailsByRecipeId(recipeId);

                if (recipeDetails == null || !recipeDetails.Any())
                {
                    return NotFound(new { Message = "Không tìm thấy RecipeDetails nào cho công thức này." });
                }

                var recipeDetailViewDtos = recipeDetails.Select(detail => new RecipeDetailViewDto
                {
                    IngredientName = detail.Ingredient.IngredientName,
                    Quantity = detail.Quantity
                }).ToList();

                return Ok(recipeDetailViewDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi server nội bộ", Error = ex.Message });
            }
        }
        [HttpPost("create-or-update-recipe-details")]
        public async Task<IActionResult> CreateOrUpdateRecipeDetails([FromBody] List<RecipeDetailDto> recipeDetails, [FromQuery] int recipeId)
        {
            try
            {
                if (recipeDetails == null || !recipeDetails.Any())
                {
                    return BadRequest(new { Message = "Danh sách RecipeDetails không được để trống." });
                }

                // Xóa tất cả RecipeDetails cũ của công thức
                await _productRepository.DeleteRecipeDetailsByRecipeId(recipeId);

                // Thêm mới danh sách RecipeDetails
                var recipeDetailEntities = recipeDetails.Select(detail => new RecipeDetail
                {
                    RecipeId = recipeId,
                    IngredientId = detail.IngredientId,
                    Quantity = detail.Quantity
                }).ToList();

                await _productRepository.AddRecipeDetails(recipeDetailEntities);

                return StatusCode(201, new
                {
                    RecipeId = recipeId,
                    RecipeDetailsCount = recipeDetailEntities.Count,
                    Message = "RecipeDetails đã được cập nhật thành công!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi server nội bộ", Error = ex.Message });
            }
        }

        [HttpPost("create-recipe-detail")]
        public async Task<IActionResult> CreateRecipeDetail([FromBody] List<RecipeDetailDto> recipeDetails, [FromQuery] int recipeId)
        {
            try
            {
                if (recipeDetails == null || !recipeDetails.Any())
                {
                    return BadRequest(new { Message = "Danh sách RecipeDetails không được để trống." });
                }

                var recipeDetailEntities = recipeDetails.Select(detail => new RecipeDetail
                {
                    RecipeId = recipeId,
                    IngredientId = detail.IngredientId,
                    Quantity = detail.Quantity
                }).ToList();

                await _productRepository.AddRecipeDetails(recipeDetailEntities);

                return StatusCode(201, new
                {
                    RecipeId = recipeId,
                    RecipeDetailsCount = recipeDetailEntities.Count,
                    Message = "RecipeDetails đã được tạo thành công!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi server nội bộ", Error = ex.Message });
            }
        }

        [HttpGet("all-category")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NoContent();  
            }

            return Ok(categories); 
        }
        [HttpGet("all-ingredient")]
        public async Task<IActionResult> GetIngredients()
        {
            var ingres = await _context.Ingredients.AsNoTracking().ToListAsync();

            if (ingres == null || ingres.Count == 0)
            {
                return NoContent();  
            }

            return Ok(ingres);  
        }


        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();

            foreach (var product in products)
            {
                product.Image = $"{Request.Scheme}://{Request.Host}/{product.Image}";
            }

            return Ok(products);

        }
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = _mapper.Map<Product>(productDto);
                 
                
                if (productDto.Image != null && productDto.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/products");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDto.Image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.Image.CopyToAsync(stream);
                    }

                    product.Image = $"uploads/products/{fileName}";
                }

                var createdProduct = await _productRepository.CreateProduct(product);

                if (createdProduct == null)
                {
                    return StatusCode(500, new { Message = "Failed to create product." });
                }

                return StatusCode(200, new
                {
                    ProductName = createdProduct.ProductName,
                    ImageUrl = createdProduct.Image,
                    RecipeId = createdProduct.RecipeId,
                    Message = "Product and Recipe created successfully!"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = e.Message });
            }
        }

        [HttpGet("get-product/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductById(productId);
                
                product.Image = $"{Request.Scheme}://{Request.Host}/{product.Image}";
                

                if (product == null)
                {
                    return NotFound(new { Message = "Product not found." });
                }

                var productDto = _mapper.Map<ProductEditDto>(product);

                return Ok(productDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = e.Message });
            }
        }


 

    }
}
