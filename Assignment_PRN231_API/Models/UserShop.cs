using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class UserShop
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ShopId { get; set; }
        public string Role { get; set; } = null!;

        public virtual Shop Shop { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}
