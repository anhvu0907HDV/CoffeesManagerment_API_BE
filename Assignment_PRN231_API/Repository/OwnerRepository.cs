using api_VS.Data;
using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
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
        public async Task<ManagerAddDto> CreateUser(ManagerAddDto user)
        {

            var newUser = _mapper.Map<AppUser>(user);
            // 🔹 Xử lý lưu ảnh nếu có
            if (user.Avatar != null && user.Avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/avata");
                Directory.CreateDirectory(uploadsFolder); 

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(user.Avatar.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await user.Avatar.CopyToAsync(stream);
                }

                newUser.Avatar = $"uploads/avata/{fileName}";  
            }
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
                    var managerDto = _mapper.Map<ListManagerDto>(user);
                    managerDto.Avatar = user.Avatar ?? "/uploads/avata/default-avatar.png";
                    managers.Add(managerDto);
                }
            }
            return managers;
        }

        public async Task<List<ShopDto>> GetAllShop()
        {
            var shops = await _context.Shops.ToListAsync();
            var shopsDto = _mapper.Map<List<ShopDto>>(shops);

            foreach (var shop in shopsDto)
            {
                var staffs = await _context.Users.Include(s => s.UserShops).ThenInclude(s => s.Shop).Where(s => s.UserShops.Any(s => s.ShopId == shop.ShopId)).ToListAsync();

                shop.Staffs = _mapper.Map<List<StaffOwnerDto>>(staffs);

            }

            if (shops == null) return null;

            return shopsDto;

        }

        public async Task<List<StaffOwnerDto>> GetAllStaff()
        {

            var users = await _userManager.Users.Include(s => s.UserShops).ThenInclude(s => s.Shop).ToListAsync();
            if (users == null) return null;

            var staffs = new List<StaffOwnerDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Count == 0 || roles.Any(r => r == "Staff"))
                {
                    var staffDto = _mapper.Map<StaffOwnerDto>(user);
                    staffDto.Avatar = user.Avatar ?? "/uploads/avata/default-avatar.png";
                    staffs.Add(staffDto);
                }
            }
            return staffs;

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

        public async Task<ManagerEditDto?> UpdateUser(ManagerEditDto user, Guid managerId)
        {
            var existingUser = await _userManager.FindByIdAsync(managerId.ToString());
            if (existingUser == null)
            {
                return null; // Trả về null nếu không tìm thấy user
            }

            if (user.Avatar != null && user.Avatar.Length > 0 && !string.IsNullOrEmpty(existingUser.Avatar))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingUser.Avatar);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            // 🔹 Ánh xạ dữ liệu từ `ManagerEditDto` sang `AppUser` (trừ Avatar)
            _mapper.Map(user, existingUser);

            // 🔹 Xử lý lưu Avatar mới nếu có
            if (user.Avatar != null && user.Avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/avata");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(user.Avatar.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await user.Avatar.CopyToAsync(stream);
                }

                existingUser.Avatar = $"uploads/avata/{fileName}";
            }

            if (user.ShopId != null)
            {
                var userShop = await _context.UserShops.FirstOrDefaultAsync(us => us.UserId == existingUser.Id);
                if (userShop != null)
                {
                    userShop.ShopId = user.ShopId.Value;
                    _context.UserShops.Update(userShop);
                }
                else
                {
                    await _context.UserShops.AddAsync(new UserShop
                    {
                        UserId = existingUser.Id,
                        ShopId = user.ShopId.Value,
                        Role = "Manager"
                    });
                }
            }

            var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }

    }
}
