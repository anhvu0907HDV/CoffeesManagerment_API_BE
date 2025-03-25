namespace Assignment_PRN231_API.DTOs.Staff
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } = null!;
        public int TableId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
        public string PaymentId { get; set; } = null!;


    }
}
