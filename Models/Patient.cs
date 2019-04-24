using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DentalEstrada.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }

        public DateTime Appointment { get; set; }


    }
}