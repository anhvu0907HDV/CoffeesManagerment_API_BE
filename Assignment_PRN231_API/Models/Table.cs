using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Table
    {
        public int TableId { get; set; }
        public bool Status { get; set; }
        public int ShopId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Shop Shop { get; set; } = null!;
    }
}
