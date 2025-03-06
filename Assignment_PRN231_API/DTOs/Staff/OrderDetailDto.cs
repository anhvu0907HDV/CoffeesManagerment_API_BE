namespace Assignment_PRN231_API.DTOs.Staff
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
