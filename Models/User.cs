using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DentalEstrada.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Email:     ")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password:        ")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        [MaxLength(255)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "1 Letter, 1 Special, 1 Number")]

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        public string Confirm { get; set; }

    }
}