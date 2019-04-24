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

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CustomerName { get; set; }

        public string Description { get; set; }

        public int Phone { get; set; }


    }
}