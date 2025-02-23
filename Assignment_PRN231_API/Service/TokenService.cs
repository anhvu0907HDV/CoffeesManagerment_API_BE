﻿using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Assignment_PRN231_API.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));

            
        }
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,appUser.UserName)
            };
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescreptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescreptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
