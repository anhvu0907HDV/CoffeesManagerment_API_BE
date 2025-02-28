using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Assignment_PRN231_API.Models
{
    public class AppUser :IdentityUser
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public string? Sex { get; set; }
        public string? PhoneNo { get; set; }
        public int? Age { get; set; } 
        public DateTime? Birthday { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<UserShop> UserShops { get; set; }

    }
}
