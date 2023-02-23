using Microsoft.EntityFrameworkCore;

namespace HealthCare.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; } 

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ApplicationDetails> ApplicationDetails { get; set; }

        public DbSet<AppointmentStatus> AppointmentsStatus { get; set;}

        public DbSet<Specialization> Specializations { get; set; }
    }
}
