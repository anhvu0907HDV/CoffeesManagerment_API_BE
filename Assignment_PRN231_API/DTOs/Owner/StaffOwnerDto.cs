﻿using System.ComponentModel.DataAnnotations;

namespace Assignment_PRN231_API.DTOs.Owner
{
    public class StaffOwnerDto
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
