using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null)  return Unauthorized("Invalid username!");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) {
                return Unauthorized("Username not found and/or password not true");
            }

            return Ok(new NewUserDto
            {
                Username = loginDto.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });



        } 
            
        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,

                };
                var createUser = await _userManager.CreateAsync(appUser,registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Manager");
                    if (roleResult.Succeeded) { 
                        return Ok(new NewUserDto
                        {
                            Email = registerDto.Email,
                            Password = registerDto.Password,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }

            }
            catch (Exception e) {
                return StatusCode(500, e);
            }
        }

    }
}
