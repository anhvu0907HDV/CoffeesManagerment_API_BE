
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Product;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;


namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<ListProductDto>> GetAllProducts();

        Task<Product?> CreateProduct(Product product);
        
        Task<ProductDto> UpdateProduct(ProductDto productDto);

        Task AddRecipeDetails(List<RecipeDetail> recipeDetails);
        Task<List<RecipeDetail>> GetRecipeDetailsByRecipeId(int recipeId);
        Task<Product?> GetProductById(int productId);
        Task DeleteRecipeDetailsByRecipeId(int recipeId);
        Task<Product> UpdateProduct(Product product);

    }
}
