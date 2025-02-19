using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public string PaymentId { get; set; } = null!;
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
