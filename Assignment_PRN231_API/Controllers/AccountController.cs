using api_VS.Data;
using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Assignment_PRN231_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager, IMapper mapper, ApplicationDBContext _context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mapper = mapper;
            this._context = _context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Username not found and/or password not true");

            var roles = await _userManager.GetRolesAsync(user);

            // 🔹 Tạo Access Token
            var accessToken = _tokenService.CreateToken(user, roles.ToList());

            // 🔹 Tạo Refresh Token

            var refreshToken = _tokenService.GenerateRefreshToken();

            // 🔹 Lưu Refresh Token vào Database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Hết hạn sau 7 ngày
            await _userManager.UpdateAsync(user);

            return Ok(new 
            {
                Avatar = $"{Request.Scheme}://{Request.Host}/{user.Avatar}",
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Token = accessToken,
                RefreshToken = refreshToken, 
                Roles = roles.ToList()
            });



        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // 🔹 Xóa Refresh Token khỏi database
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            await _userManager.UpdateAsync(user);

            return Ok("Logged out");
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            var roles = await _userManager.GetRolesAsync(user);

            // 🔹 Tạo Access Token mới
            var newAccessToken = _tokenService.CreateToken(user, roles.ToList());

            // 🔹 Tạo Refresh Token mới
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // 🔹 Cập nhật Refresh Token mới vào Database
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = _mapper.Map<AppUser>(registerDto);

                // 🔹 Xử lý lưu ảnh nếu có
                if (registerDto.Avatar != null && registerDto.Avatar.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/avata");
                    Directory.CreateDirectory(uploadsFolder); // Đảm bảo thư mục tồn tại

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(registerDto.Avatar.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await registerDto.Avatar.CopyToAsync(stream);
                    }

                    appUser.Avatar = $"uploads/avata/{fileName}"; // Lưu đường dẫn vào DB
                }

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createUser.Succeeded)
                {
                    await _context.UserShops.AddAsync(new UserShop
                    {
                        UserId = appUser.Id,
                        ShopId = registerDto.ShopId,
                        Role = "unknow"
                    });
                    await _context.SaveChangesAsync();

                    return StatusCode(200, new
                    {
                        Email = appUser.Email,
                        Avatar = appUser.Avatar, // Trả về đường dẫn ảnh
                        Message = "User created successfully!"
                    });
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
