namespace Assignment_PRN231_API.DTOs.Staff
{
    public class TableForOrderDTO
    {
        public int TableId { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        // Tính toán trạng thái bàn cho Frontend (Busy/Available)
        //public string StatusDisplay => Status ? "Busy" : "Available";
    }
}
