using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DentalEstrada.Models
{
    public class Inquiry
    {
        [Key]
        public int InquiryID { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid Number")]
        public string Phone { get; set; }

        [Required]
        [MinLength(2)]
        public string Email { get; set; }

        public string Message { get; set; }

        public string BestTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}