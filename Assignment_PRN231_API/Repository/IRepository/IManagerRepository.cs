using Assignment_PRN231_API.DTOs.Staff;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IManagerRepository
    {
        Task<List<StaffDto>> GetAllStaffByShopId(int shopId);
        Task<int?> GetShopIdByEmailAsync(string email);
    }
}
