using Assignment_PRN231_API.DTOs.Order;

namespace Assignment_PRN231_API.DTOs.Staff
{
    public class UpdateOrderDto
    {
        public string UserId { get; set; } = null!;
        public DateTime? OrderDate { get; set; }
        public string OrderStatus { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public int TableId { get; set; }
        public List<OrderDetailInputDto> OrderDetails { get; set; } = new List<OrderDetailInputDto>();
    }
}
