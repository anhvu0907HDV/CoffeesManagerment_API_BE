using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Owner
{
    public class StaffOwnerDto
    {
        public string? Id { get; set; } // Không cần nullable nếu ID luôn có
        public string AvatarUrl { get; set; } = string.Empty; // Đảm bảo giá trị mặc định
        public string FullName { get; set; } = string.Empty;
        public string ShopName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        public string PhoneNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose your gender")]
        public string Sex { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shop is required")]
        public int ShopId { get; set; } // ShopId thường không nên nullable

        [Required(ErrorMessage = "Role is required")]
        public string RoleName { get; set; } = string.Empty;
    }
}
