using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class AppointmentStatus
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
