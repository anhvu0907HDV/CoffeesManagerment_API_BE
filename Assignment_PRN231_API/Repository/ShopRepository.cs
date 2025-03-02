using api_VS.Data;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationDBContext _context;
        private IMapper _mapper;
        public ShopRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopDto> CreateShop(ShopDto shopDto)
        {
            var shop = _mapper.Map<Shop>(shopDto);

            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync(); // Lưu vào database

            // Cập nhật DTO với ID mới
            var result = _mapper.Map<ShopDto>(shop);
            return result;

        }

        public Task<bool> DeleteShop(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShopDto>> GetAllShops()
        {
            if (_context.Shops == null)
            {
                return new List<ShopDto>();
            }

            var shops = await _context.Shops.ToListAsync();
            return _mapper.Map<List<ShopDto>>(shops);
        }

        public async Task<ShopDto> GetShopById(int id)
        {
            var shop = await _context.Shops.FirstOrDefaultAsync(x => x.ShopId == id);
            return _mapper.Map<ShopDto>(shop);

        }

        public async Task<ShopDto> UpdateShop(ShopDto shopDto)
        {
            var shop = _mapper.Map<Shop>(shopDto);
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShopDto>(shop);
        }
    }
}
