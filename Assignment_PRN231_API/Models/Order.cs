using System;
using System.Collections.Generic;

namespace Assignment_PRN231_API.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = null!;
        public string PaymentId { get; set; } = null!;

        public virtual Payment Payment { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
