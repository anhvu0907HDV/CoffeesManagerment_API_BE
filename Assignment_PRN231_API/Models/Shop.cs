using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Tables = new HashSet<Table>();
            UserShops = new HashSet<UserShop>();
        }

        public int ShopId { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Table> Tables { get; set; }
        public virtual ICollection<UserShop> UserShops { get; set; }
    }
}
