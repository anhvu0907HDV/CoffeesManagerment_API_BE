using Asignment_PRN231_API_FE.ViewModel.AnotationCustom;
using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel
{
    public class AddProductVM
    {
        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Công thức sản phẩm là bắt buộc.")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
        public string? ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Mô tả sản phẩm là bắt buộc.")]
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0.")]
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        [Required(ErrorMessage = "Size là bắt buộc.")]
        public int? Size { get; set; }
        public int? Quantity { get; set; }
        public int? IsActive { get; set; }
    }
}
