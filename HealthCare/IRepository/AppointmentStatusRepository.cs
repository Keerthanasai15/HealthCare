using HealthCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.IRepository
{
    public class AppointmentStatusRepository : IRepositoryStatus<AppointmentStatus>
    {
        private readonly ApplicationDbContext _context5;

        public AppointmentStatusRepository(ApplicationDbContext context5)
        {
            _context5 = context5;
        }

        public async Task<IActionResult> Create(AppointmentStatus status)
        {
            if (status != null)
            {
                _context5.AppointmentsStatus.Add(status);
                await _context5.SaveChangesAsync();
            }
            return null;
        }

        public async  Task<AppointmentStatus> Delete(int id)
        {
            var StatusInDb = await _context5.AppointmentsStatus.FindAsync(id);
            if (StatusInDb != null)
            {
                _context5.AppointmentsStatus.Remove(StatusInDb);
                await _context5.SaveChangesAsync();
                return StatusInDb;
            }
            return null;

        }

        public async Task<ActionResult<IEnumerable<AppointmentStatus>>> GetAll()
        {
            return await _context5.AppointmentsStatus.ToListAsync();
        }

        public async  Task<ActionResult<AppointmentStatus>> GetById(int id)
        {
            var status = await _context5.AppointmentsStatus.FindAsync(id);
            if (status != null)
            {
                return status;
            }
            return null;
        }

        public async Task<AppointmentStatus> Update(int id, AppointmentStatus status)
        {
            var statusInDb = await _context5.AppointmentsStatus.FindAsync(id);
            if (statusInDb != null)
            {
                statusInDb.StatusName = status.StatusName;
                _context5.AppointmentsStatus.Update(statusInDb);
                await _context5.SaveChangesAsync();
                return statusInDb;
            }
            return null;

        }
    }
}
