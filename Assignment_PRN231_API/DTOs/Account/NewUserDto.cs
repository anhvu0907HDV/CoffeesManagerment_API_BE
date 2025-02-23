using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Account
{
    public class NewUserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Token { get; set; }
    }
}
