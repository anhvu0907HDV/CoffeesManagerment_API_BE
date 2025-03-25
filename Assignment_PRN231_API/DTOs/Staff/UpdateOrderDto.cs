namespace Assignment_PRN231_API.DTOs.Staff
{
    public class UpdateOrderDto
    {
        public string? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentId { get; set; }
    }
}
