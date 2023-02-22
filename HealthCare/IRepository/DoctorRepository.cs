using HealthCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.IRepository
{
    public class DoctorRepository : IRepositoryDoctor<Doctor>
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (doctor != null)
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }
            return null;

        }

        public async Task<Doctor> Delete(int id)
        {
            var emp = await _context.Doctors.FindAsync(id);
            if (emp != null)
            {
                _context.Doctors.Remove(emp);
                await _context.SaveChangesAsync();
                return emp;
            }

            return null;
        }

        public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async  Task<ActionResult<Doctor>> GetById(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);
            if (doc != null)
            {
                return doc;
            }
            return null;

        }

        public async Task<Doctor> Update(int id, Doctor doc)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor.IsApproved = doc.IsApproved;
                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync();
                return doctor;
            }
            return null;
        }
    }
}
