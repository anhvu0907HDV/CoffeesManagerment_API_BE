using api_VS.Data;
using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.DTOs.Staff;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public OwnerRepository(UserManager<AppUser> userManager, IMapper mapper, ApplicationDBContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
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

                if (user.ShopId != null)
                {
                    var userShop = await _context.UserShops.AddAsync(new UserShop { UserId = newUser.Id, ShopId = user.ShopId, Role = "Manager" });
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
            var users = await _userManager.Users.Include(s => s.UserShops).ThenInclude(s => s.Shop).ToListAsync();
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
                var staffs = await _context.Users
                    .Include(s => s.UserShops)
                    .ThenInclude(s => s.Shop)
                    .Where(s => s.UserShops.Any(s => s.ShopId == shop.ShopId))
                    .ToListAsync();

                shop.Staffs = _mapper.Map<List<StaffOwnerDto>>(staffs);

                // Danh sách nhân viên cần giữ lại
                var filteredStaffs = new List<StaffOwnerDto>();

                foreach (var staff in shop.Staffs)
                {
                    var user = await _userManager.FindByIdAsync(staff.Id.ToString());
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        staff.RoleName = string.Join(", ", roles); // Nếu có nhiều role, ghép chúng lại

                        // Kiểm tra nếu không có vai trò "Owner" thì giữ lại
                        if (!roles.Contains("Owner"))
                        {
                            filteredStaffs.Add(staff);
                        }
                    }
                }

                // Cập nhật lại danh sách nhân viên sau khi lọc
                shop.Staffs = filteredStaffs;

                if (shops == null) return null;
            }
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
                    if (roles.Count == 0) staffDto.RoleName = "Please set role";
                    else staffDto.RoleName = "Staff";
                    if (user.UserShops.Count == 0)
                    {
                        staffDto.ShopName = "Please select shop";
                    }
                    staffDto.AvatarUrl = user.Avatar ?? "/uploads/avata/default-avatar.png";
                    staffs.Add(staffDto);
                }
            }
            return staffs;

        }

        public async Task<ManagerDto> GetManager(Guid id)
        {
            var user = await _userManager.Users.Include(s => s.UserShops).ThenInclude(s => s.Shop).FirstOrDefaultAsync(s => s.Id.Equals(id.ToString()));

            if (user == null) return null;
            if (await _userManager.IsInRoleAsync(user, "Manager"))
            {
                return _mapper.Map<ManagerDto>(user);
            }
            return null;
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var rolesDto = await _roleManager.Roles
                        .Where(r => r.Name != "Owner")
                        .Select(r => new RoleDto
                        {
                            RoleId = r.Id,
                            RoleName = r.Name
                        })
                        .ToListAsync();
            return rolesDto;
        }

        public async Task<StaffEditDto> GetStaffById(Guid Id)
        {
            var user = await _userManager.Users.Include(s => s.UserShops).ThenInclude(s => s.Shop).FirstOrDefaultAsync(s => s.Id.Equals(Id.ToString()));

            if (user == null) return null;
            var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var staff = _mapper.Map<StaffEditDto>(user);
            if (!string.IsNullOrEmpty(roleName))
            {
                // Lấy RoleId từ bảng AspNetRoles dựa vào roleName
                var role = await _roleManager.FindByNameAsync(roleName);
                staff.RoleId = role?.Id;
            }
            return staff;
        }

        public async Task<StaffEditDto> UpdateStaff(StaffEditDto user, Guid staffId)
        {
            var existingUser = await _userManager.FindByIdAsync(staffId.ToString());
            if (existingUser == null) return null;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 🔹 Kiểm tra và cập nhật Avatar nếu có
                if (user.Avatar != null && user.Avatar.Length > 0)
                {
                    var uploadsFolder = Path.Combine("wwwroot/uploads/avata");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(user.Avatar.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await user.Avatar.CopyToAsync(stream);

                    // Xóa avatar cũ nếu có
                    if (!string.IsNullOrEmpty(existingUser.Avatar))
                    {
                        var oldFilePath = Path.Combine("wwwroot", existingUser.Avatar);
                        if (File.Exists(oldFilePath)) File.Delete(oldFilePath);
                    }

                    existingUser.Avatar = $"uploads/avata/{fileName}";
                }

                // 🔹 Cập nhật thông tin người dùng
                _mapper.Map(user, existingUser);
                var updateUserResult = await _userManager.UpdateAsync(existingUser);
                if (!updateUserResult.Succeeded) return null;

                // 🔹 Cập nhật ShopId nếu có
                if (user.ShopId.HasValue)
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
                            Role = "Staff"
                        });
                    }
                }

                // 🔹 Cập nhật Role nếu có
                if (!string.IsNullOrEmpty(user.RoleId))
                {
                    var role = await _roleManager.FindByIdAsync(user.RoleId);
                    if (role != null)
                    {
                        var currentRoles = await _userManager.GetRolesAsync(existingUser);
                        if (currentRoles.Any())
                        {
                            var removeRolesResult = await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
                            if (!removeRolesResult.Succeeded) return null;
                        }

                        var addRoleResult = await _userManager.AddToRoleAsync(existingUser, role.Name);
                        if (!addRoleResult.Succeeded) return null;
                    }
                }

                // 🔹 Lưu thay đổi vào database
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return user;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }


        public async Task<ManagerEditDto?> UpdateUser(ManagerEditDto user, Guid managerId)
        {
            var existingUser = await _userManager.FindByIdAsync(managerId.ToString());
            if (existingUser == null) return null;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 🔹 Kiểm tra và cập nhật Avatar nếu có
                if (user.Avatar is { Length: > 0 })
                {
                    var uploadsFolder = Path.Combine("wwwroot/uploads/avata");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(user.Avatar.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await user.Avatar.CopyToAsync(stream);

                    // Xóa avatar cũ nếu có
                    if (!string.IsNullOrEmpty(existingUser.Avatar))
                    {
                        var oldFilePath = Path.Combine("wwwroot", existingUser.Avatar);
                        if (File.Exists(oldFilePath)) File.Delete(oldFilePath);
                    }

                    existingUser.Avatar = $"uploads/avata/{fileName}";
                }

                // 🔹 Cập nhật thông tin người dùng
                _mapper.Map(user, existingUser);
                var updateUserResult = await _userManager.UpdateAsync(existingUser);
                if (!updateUserResult.Succeeded) return null;

                // 🔹 Cập nhật ShopId nếu có
                if (user.ShopId.HasValue)
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

                // 🔹 Lưu thay đổi vào database
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return user;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }


    }
}
