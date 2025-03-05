using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Service.IService
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(int id, Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
    }
}
