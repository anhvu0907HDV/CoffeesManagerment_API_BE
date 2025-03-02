using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IShopRepository
    {
        Task<List<ShopDto>> GetAllShops();
        Task<ShopDto> GetShopById(int id);
        Task<ShopDto> CreateShop(ShopDto shopDto);
        Task<ShopDto> UpdateShop(ShopDto shopDto);
        Task<bool> DeleteShop(int id);

    }
}
