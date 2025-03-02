using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IOwnerRepository
    {
        Task<List<ListManagerDto>> GetAllManager();
        Task<ManagerDto> GetManager(Guid id);
        Task<ManagerEditDto> CreateUser(ManagerEditDto manager);
        Task<AppUser> UpdateUser(AppUser user);
        Task<AppUser> DeleteUser(Guid id);

    }
}
