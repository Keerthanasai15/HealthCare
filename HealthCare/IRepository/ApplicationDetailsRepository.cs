using HealthCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.IRepository
{
    public class ApplicationDetailsRepository : IRepositoryADR<ApplicationDetails>
    {
        private readonly ApplicationDbContext _context2;

        public ApplicationDetailsRepository(ApplicationDbContext context2)
        {
            _context2 = context2;
        }



        public async Task<IActionResult> Create(ApplicationDetails applicationDetails)
        {
            if (applicationDetails != null)
            {
                _context2.ApplicationDetails.Add(applicationDetails);
                await _context2.SaveChangesAsync();
            }
            return null;
        }

        public async  Task<ApplicationDetails> Delete(int id)
        {
            var applicationDetailsInDb = await _context2.ApplicationDetails.FindAsync(id);
            if (applicationDetailsInDb != null)
            {
                _context2.Remove(applicationDetailsInDb);
                await _context2.SaveChangesAsync();
                return applicationDetailsInDb;
            }
            return null;

        }

        public async  Task<ActionResult<IEnumerable<ApplicationDetails>>> GetAll()
        {
            return await _context2.ApplicationDetails.ToListAsync();
        }

        public async Task<ActionResult<ApplicationDetails>> GetById(int id)
        {
            var applicationDetails = await _context2.ApplicationDetails.FindAsync(id);
            if (applicationDetails != null)
            {
                return applicationDetails;
            }
            return null;
        }

        public async Task<ApplicationDetails> Update(int id, ApplicationDetails applicationDetails)
        {
            var applicationDetailsInDb = await _context2.ApplicationDetails.FindAsync(id);
            if (applicationDetailsInDb != null)
            {
                applicationDetailsInDb.ApplicationDate = applicationDetails.ApplicationDate;
                applicationDetailsInDb.ApplicationId = applicationDetails.ApplicationId;
               
                applicationDetailsInDb.AppointmentStatus = applicationDetails.AppointmentStatus;
                applicationDetailsInDb.AppointmentStatusId = applicationDetails.AppointmentStatusId;
               

     
                _context2.ApplicationDetails.Update(applicationDetailsInDb);
                await _context2.SaveChangesAsync();
                return applicationDetailsInDb;
            }
            return null;
        }

    }
    }

