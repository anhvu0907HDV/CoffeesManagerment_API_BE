using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class TableOrder
    {
        public int TableId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Table Table { get; set; } = null!;
    }
}
