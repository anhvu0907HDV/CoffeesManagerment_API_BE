namespace Asignment_PRN231_API_FE.ViewModel
{
	public class OrderDetailVM
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
		public int? Quantity { get; set; }
		public decimal? SubTotal { get; set; }
	}
}
