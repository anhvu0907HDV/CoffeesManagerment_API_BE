using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel
{
    public class StaffListVM
    {
        public Guid? Id { get; set; }
        public string? AvatarUrl { get; set; }
        public string? FullName { get; set; }
        public string? ShopName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "Please choose your gender")]
        public string? Sex { get; set; }
        [Required(ErrorMessage = "Shop is required")]
        public int? ShopId { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string? RoleName { get; set; }
    }
}
