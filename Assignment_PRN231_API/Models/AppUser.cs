﻿using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Assignment_PRN231_API.Models
{
    public class AppUser :IdentityUser
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Avatar { get; set; }

        public int? Age { get; set; } 
        public DateTime? Birthday { get; set; } 

    }
}
