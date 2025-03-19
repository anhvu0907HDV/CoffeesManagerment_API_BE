using api_VS.Data;

using Assignment_PRN231_API.DTOs.Owner;
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


        public async Task<List<ProductDto>> GetAllProducts()
        {
            if(_context.Products == null)
            {
                return new List<ProductDto>();
            }

            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }


        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); // Lưu vào database

            // Cập nhật DTO với ID mới
            var result = _mapper.Map<ProductDto>(product);
            return result;

        }


        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }
    }
}
