using api_VS.Data;

using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Product;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        private IMapper _mapper;
        public ProductRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<List<RecipeDetail>> GetRecipeDetailsByRecipeId(int recipeId)
        {
            return await _context.RecipeDetails.Include(i=>i.Ingredient)
                                 .Where(rd => rd.RecipeId == recipeId)
                                 .ToListAsync();
        }
        public async Task AddRecipeDetails(List<RecipeDetail> recipeDetails)
        {
            await _context.RecipeDetails.AddRangeAsync(recipeDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ListProductDto>> GetAllProducts()
        {
            return await _context.Products
        .Include(p => p.Category)
        .Include(p => p.Recipes) // Để lấy công thức sản phẩm
        .Select(p => new ListProductDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Description = p.Description,
            Price = p.Price,
            Discount = p.Discount,
            Size = p.Size,
            Quantity = p.Quantity,
            IsActive = p.IsActive,
            Image = p.Image,
            CategoryName = p.Category.CategoryName,
            RecipeDescription = p.Recipes.FirstOrDefault().Description ?? "No recipe"
        })
        .ToListAsync();
        }

        public async Task<Product?> CreateProduct(Product product)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Bước 1: Thêm Product
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Bước 2: Tạo Recipe
                var recipe = new Recipe
                {
                    ProductId = product.ProductId,
                    Description = $"Công thức cho {product.ProductName}"
                };

                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                // Bước 3: Cập nhật Product với RecipeId
                product.RecipeId = recipe.RecipeId;
                await _context.SaveChangesAsync();

                // Commit transaction nếu tất cả thành công
                await transaction.CommitAsync();

                return product;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // Rollback nếu có lỗi
                throw;
            }
        }


        public async Task<Product?> GetProductById(int productId)
        {
            return await _context.Products
                .Include(p => p.Category) // Nếu muốn lấy thông tin danh mục
                .Include(p => p.Recipes) // Nếu muốn lấy danh sách nguyên liệu
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }


        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
        public async Task DeleteRecipeDetailsByRecipeId(int recipeId)
        {
            var existingDetails = _context.RecipeDetails.Where(rd => rd.RecipeId == recipeId);
            _context.RecipeDetails.RemoveRange(existingDetails);
            await _context.SaveChangesAsync();
        }
    }

}
