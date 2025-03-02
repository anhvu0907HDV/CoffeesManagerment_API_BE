using api_VS.Data;
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
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public OwnerRepository(UserManager<AppUser> userManager,IMapper mapper, ApplicationDBContext context) 
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ManagerEditDto> CreateUser(ManagerEditDto user)
        {

            var newUser = _mapper.Map<AppUser>(user);
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Manager");

                if(user.ShopId != null) {
                    var userShop = await _context.UserShops.AddAsync(new UserShop {UserId = newUser.Id, ShopId = user.ShopId,Role="Manager" });
                    await _context.SaveChangesAsync();
                }

                return user;

            }
            return null;

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

        public async Task<ManagerDto> GetManager(Guid id)
        {
            var user  = await _userManager.Users.Include(s => s.UserShops).ThenInclude(s => s.Shop).FirstOrDefaultAsync(s => s.Id.Equals(id.ToString()));
            
            if (user == null) return null;
            if (await _userManager.IsInRoleAsync(user, "Manager"))
            {
                return _mapper.Map<ManagerDto>(user);
            }
            return null;
        }

        public Task<AppUser> UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
