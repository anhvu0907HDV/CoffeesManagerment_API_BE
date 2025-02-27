using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IOwnerRepository
    {
        Task<List<ListManagerDto>> GetAllManager();
        Task<AppUser> GetUser(Guid id);
        Task<AppUser> CreateUser(AppUser user);
        Task<AppUser> UpdateUser(AppUser user);
        Task<AppUser> DeleteUser(Guid id);

    }
}
