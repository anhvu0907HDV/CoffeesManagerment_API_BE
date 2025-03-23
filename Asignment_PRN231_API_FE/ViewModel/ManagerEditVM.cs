using Asignment_PRN231_API_FE.ViewModel.AnotationCustom;
using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel
{
    public class ManagerEditVM
    {
        public Guid Id { get; set; }
        public string?  AvatarUrl { get; set; }
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? Avatar { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "Please choose your gender")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Please select a shop")]
        public int? ShopId { get; set; }
    }
}
