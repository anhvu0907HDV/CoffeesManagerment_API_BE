using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel
{
    public class TableVM
    {
        public int Id { get; set; }

        [Required]
        public bool Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ShopId must be a positive integer.")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty.")]
        [StringLength(100, ErrorMessage = "Table name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
