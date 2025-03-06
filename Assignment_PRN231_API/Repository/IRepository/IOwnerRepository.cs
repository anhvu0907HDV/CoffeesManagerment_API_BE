using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IOwnerRepository
    {
        Task<List<ListManagerDto>> GetAllManager();
        Task<ManagerDto> GetManager(Guid id);
        Task<ManagerAddDto> CreateUser(ManagerAddDto manager);
        Task<ManagerEditDto> UpdateUser(ManagerEditDto user,Guid managerId);
        Task<AppUser> DeleteUser(Guid id);
        Task<List<StaffOwnerDto>> GetAllStaff();
        Task<List<ShopDto>> GetAllShop();

    }
}
