using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mapper = mapper;
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
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new NewUserDto
            {
                Username = loginDto.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, roles.ToList()),
                Roles = roles.ToList()
            });



        } 
            
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = _mapper.Map<AppUser>(registerDto);
                var createUser = await _userManager.CreateAsync(appUser,registerDto.Password);

                if (createUser.Succeeded)
                {
                    return StatusCode(200 ,new
                    {
                        Email = appUser.Email,
                        Message = "User created successfully!"
                    });
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
