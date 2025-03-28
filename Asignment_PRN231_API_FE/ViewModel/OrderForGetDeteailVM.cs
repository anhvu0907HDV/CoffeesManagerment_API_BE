namespace Asignment_PRN231_API_FE.ViewModel
{
    public class OrderForGetDeteailVM
    {
        public int OrderId { get; set; }
        public List<UserGetVM> Users { get; set; } = new List<UserGetVM>();
        public int TableId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = null!;
        public PaymentVM Payment { get; set; } = null!;
        public List<OrderDetailVM> OrderDetails { get; set; } = new List<OrderDetailVM>();
        public TableVM? Table { get; set; }

    }
}
