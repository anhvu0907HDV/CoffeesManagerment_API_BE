using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Manager
{
    public class TableDto
    {
        [Required]
        public bool Status { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "ShopId must be a positive integer.")]
        public int ShopId { get; set; }
        [Required(ErrorMessage = "Name cannot be null or empty.")]
        [StringLength(100, ErrorMessage = "Table name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
