
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;


namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProducts();

        Task<ProductDto> CreateProduct(ProductDto productDto);

        Task<ProductDto> UpdateProduct(ProductDto productDto);
        Task<Product?> GetProductById(int productId);
        Task<bool> UpdateProductStatus(int productId, bool isActive);

    }
}
