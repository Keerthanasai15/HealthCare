using HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.IRepository
{
    public class AppointmentRepository : IAPRepository<Appointment>
    {
        private readonly ApplicationDbContext _context;


        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Appointment obj)
        {
            if (obj != null)
            {
                _context.Appointments.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Appointment> Delete(int id)
        {
            var HospitalDb1 = await _context.Appointments.FindAsync(id);
            if (HospitalDb1 != null)
            {
                _context.Appointments.Remove(HospitalDb1);
                await _context.SaveChangesAsync();
                return HospitalDb1;
            }
            return null;
        }

        public IEnumerable<Appointment> GetAll()
        {
            var appointments = _context.Appointments.Select(x => new Appointment
            {
                Id = x.Id,
                PatientName = x.PatientName,
                Gender = x.Gender,
                Age = x.Age,
                AppointmentDate = x.AppointmentDate,
                TimeSessions = x.TimeSessions,
                SpecializationOfDoctor = x.SpecializationOfDoctor,
                DoctorName = x.DoctorName,
            }).ToList();
            return appointments;
        }

        public async Task<Appointment> GetById(int id)
        {
            var appointments = await _context.Appointments.Select(x => new Appointment
            {
                Id = x.Id,
                PatientName = x.PatientName,
                Gender = x.Gender,
                Age = x.Age,
                AppointmentDate = x.AppointmentDate,
                TimeSessions = x.TimeSessions,
                SpecializationOfDoctor = x.SpecializationOfDoctor,
                DoctorName = x.DoctorName,
            }).ToListAsync();

            var appointment = appointments.FirstOrDefault(x => x.Id == id);

            if (appointment != null)
            {
                return appointment;
            }
            return null;


        }

        public async Task<Appointment> Update(int id, Appointment obj)
        {
            var HospitalDb1 = await _context.Appointments.FindAsync(id);
            if (HospitalDb1 != null)
            {
                HospitalDb1.PatientName = obj.PatientName;
                HospitalDb1.Gender = obj.Gender;
                HospitalDb1.Age = obj.Age;
                HospitalDb1.AppointmentDate = obj.AppointmentDate;
                HospitalDb1.TimeSessions = obj.TimeSessions;
                HospitalDb1.SpecializationOfDoctor = obj.SpecializationOfDoctor;
                HospitalDb1.DoctorName = obj.DoctorName;
                _context.Appointments.Update(HospitalDb1);
                await _context.SaveChangesAsync();
            }
            return null;
        }

    }
}

