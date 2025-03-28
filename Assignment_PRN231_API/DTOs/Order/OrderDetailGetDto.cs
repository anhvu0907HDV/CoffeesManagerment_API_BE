namespace Assignment_PRN231_API.DTOs.Order
{
    public class OrderDetailGetDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal? SubTotal { get; set; }
    }
}
