using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<Table> Tables { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserShop> UserShops { get; set; }
    }
}
