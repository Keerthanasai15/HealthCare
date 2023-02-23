using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace HealthCare.Models
{
    public class ApplicationDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]

        public DateTime ApplicationDate { get; set; }

        [Required]


        public string ApplicationId { get; set; }
        [Required]
        [DefaultValue(false)]

        public bool IsAccepted { get; set; }

        public  AppointmentStatus AppointmentStatus { get; set; }
        public int AppointmentStatusId { get; set; }
    }
}
