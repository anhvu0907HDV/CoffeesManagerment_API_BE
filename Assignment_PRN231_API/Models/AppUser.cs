using Microsoft.AspNetCore.Identity;

namespace Assignment_PRN231_API.Models
{
    public class AppUser :IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public int Age { get; set; }
        public int Birthday { get; set; }
        public int ShopId { get; set; }

    }
}
