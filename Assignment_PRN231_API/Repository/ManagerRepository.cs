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
        private readonly ApplicationDBContext _context;

        public ManagerRepository(IMapper mapper, UserManager<AppUser> userManager, ApplicationDBContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
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
        public async Task<int?> GetShopIdByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) return null; // ✅ Kiểm tra tránh lỗi null

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null; // 🔹 Không tìm thấy user

            // 🔹 Lấy ShopId từ bảng trung gian UserShops
            var shopId = await _context.UserShops
                .Where(us => us.UserId == user.Id) // 🔹 Lọc theo UserId
                .Select(us => us.ShopId) // 🔹 Lấy ShopId
                .FirstOrDefaultAsync(); // 🔹 Chỉ lấy 1 shop (trường hợp có nhiều shop, có thể lấy danh sách)

            return shopId; // 🔹 Trả về ShopId (hoặc null nếu không có)
        }
    }
}
