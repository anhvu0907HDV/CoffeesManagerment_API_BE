namespace Asignment_PRN231_API_FE.ViewModel
{
	public class OrderVM
	{
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string OrderStatus { get; set; } = string.Empty;
		public PaymentVM Payment { get; set; } = new();
		public List<OrderDetailVM> OrderDetails { get; set; } = new();
		public List<UserVM> Users { get; set; } = new();
        public string? TableName { get; set; }
    }
}
