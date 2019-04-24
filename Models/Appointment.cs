using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DentalEstrada.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        [Display(Name = "Start Date: ")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date: ")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Customer Name: ")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Description: ")]
        public string Description { get; set; }

        [Required]
        [Range(1, 9999999999999)]
        [Display(Name = "Phone Number: ")]
        public int Phone { get; set; }


    }
}