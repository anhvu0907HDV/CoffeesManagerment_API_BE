namespace Assignment_PRN231_API.DTOs.Payment
{
    public class PaymentDto
    {
        public string PaymentId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
    }
}
