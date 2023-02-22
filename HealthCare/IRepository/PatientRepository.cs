using HealthCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.IRepository
{
    public class PatientRepository : IPRepository<Patient>
    {
        private readonly ApplicationDbContext _context2;

        public PatientRepository(ApplicationDbContext context2)
        {
            _context2 = context2;


        }
        public async Task<IActionResult> Create(Patient patient)
        {

            if (patient != null)
            {
                _context2.Patients.Add(patient);
                await _context2.SaveChangesAsync();
            }
            return null;
        }

        public async Task<Patient> Delete(int id)
        {
            var PatiInDb = await _context2.Patients.FindAsync(id);
            if (PatiInDb != null)
            {
                _context2.Remove(PatiInDb);
                await _context2.SaveChangesAsync();
                return PatiInDb;
            }
            return null;
        }

        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            return await _context2.Patients.ToListAsync();
        }

        public async Task<ActionResult<Patient>> GetById(int id)
        {
            var patient = await _context2.Patients.FindAsync(id);
            if (patient != null)
            {
                return patient;
            }
            return null;
        }

        public async Task<ActionResult<IEnumerable<Patient>>> SearchByName(string name)
        {
            if (name == null)
            {
                return await _context2.Patients.ToListAsync();
            }

            return await _context2.Patients.Where(c => c.PatientName.Contains(name))
              .ToListAsync();
        }




        public async Task<Patient> Update(int Pastid, Patient patient)
        {
            var PatiInDb = await _context2.Patients.FindAsync(Pastid);
            if (PatiInDb != null)
            {
                PatiInDb.PatientName = patient.PatientName;
                PatiInDb.PatientAddress = patient.PatientAddress;
                PatiInDb.PatientPhNo = patient.PatientPhNo;
                PatiInDb.PatientEmail = patient.PatientEmail;
                PatiInDb.PatientPassword = patient.PatientPassword;
                _context2.Patients.Update(PatiInDb);
                await _context2.SaveChangesAsync();
                return PatiInDb;


            }
            return null;
        }
    }
}




