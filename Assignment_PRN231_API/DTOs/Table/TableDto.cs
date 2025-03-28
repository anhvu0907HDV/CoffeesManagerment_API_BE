namespace Assignment_PRN231_API.DTOs.Table
{
    public class TableDto
    {
        public int TableId { get; set; }
        public bool Status { get; set; }
        public int ShopId { get; set; }
        public string Name { get; set; } = null!;
    }
}
