using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IOwnerRepository
    {
        Task<List<ListManagerDto>> GetAllManager();
        Task<ManagerDto> GetManager(Guid id);
        Task<ManagerAddDto> CreateUser(ManagerAddDto manager);
        Task<ManagerEditDto> UpdateUser(ManagerEditDto user,Guid managerId);
        Task<StaffEditDto> UpdateStaff(StaffEditDto staff,Guid staffId);
        Task<AppUser> DeleteUser(Guid id);
        Task<List<StaffOwnerDto>> GetAllStaff();
        Task<List<ShopDto>> GetAllShop();
        Task<List<RoleDto>> GetRoles();
        Task<StaffEditDto> GetStaffById(Guid Id);
        Task<List<StaffDto>> GetAllStaffByShopId(int shopId);


    }
}
