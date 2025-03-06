using Asignment_PRN231_API_FE.ViewModel.AnotationCustom;
using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel
{
    public class ManagerAddVM
    {
        [Required(ErrorMessage = "Please choose your Avata.")]
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

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(20, ErrorMessage = "Password cannot be more than 20 characters long")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "Please choose your gender")]
        public string? Sex { get; set; }
        [Required(ErrorMessage = "Shop is required")]
        public int? ShopId { get; set; }

        [Required(ErrorMessage = "You must accept the terms and conditions")]
        public bool AcceptTerms { get; set; }
    }
}
