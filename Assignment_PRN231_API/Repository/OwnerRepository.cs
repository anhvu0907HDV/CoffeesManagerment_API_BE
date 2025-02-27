using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public OwnerRepository(UserManager<AppUser> userManager,IMapper mapper) {

            _userManager = userManager;
            _mapper = mapper;

        }
        public Task<AppUser> CreateUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ListManagerDto>> GetAllManager()
        {
            var users = await _userManager.Users.Include(s=> s.UserShops).ThenInclude(s=>s.Shop).ToListAsync();
            var managers = new List<ListManagerDto>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    managers.Add(_mapper.Map<ListManagerDto>(user));
                }
            }
            return managers;
        }

        public Task<AppUser> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
