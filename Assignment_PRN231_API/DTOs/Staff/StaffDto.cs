using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Staff
{
    public class StaffDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "Please choose your gender")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "You must accept the terms and conditions")]
        public int? ShopId { get; set; }
    }
}
