using api_VS.Data;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public ManagerRepository(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<StaffDto>> GetAllStaffByShopId(int shopId)
        {

            List<StaffDto> staffDtos = new List<StaffDto>();

            var staffUserIds = (await _userManager.GetUsersInRoleAsync("Staff"))
                                .Select(u => u.Id)
                                .ToHashSet();  

            var userInShop = await _userManager.Users
                .Include(u => u.UserShops)
                .ThenInclude(us => us.Shop)
                .Where(s => s.UserShops.Any(us => us.ShopId == shopId) && staffUserIds.Contains(s.Id))
                .ToListAsync();

            return _mapper.Map<List<StaffDto>>(userInShop);
        }
    }
}
