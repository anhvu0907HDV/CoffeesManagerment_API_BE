using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Account
{
    public class LoginDto
    {
        [Required] 
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}