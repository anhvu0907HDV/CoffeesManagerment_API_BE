using Assignment_PRN231_API.DTOs.Payment;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.DTOs.User;

namespace Assignment_PRN231_API.DTOs.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public List<UserGetDto> Users { get; set; } = new List<UserGetDto>();
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = null!;
        public PaymentDto Payment { get; set; } = null!;
        public List<OrderDetailGetDto> OrderDetails { get; set; } = new List<OrderDetailGetDto>();
    }
}
