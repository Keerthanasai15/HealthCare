using HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.IRepository
{
    public class SpecializationRepository : ISRepository<Specialization>
    {

        private readonly ApplicationDbContext _context;
        public SpecializationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Specialization obj)
        {
            if (obj != null)
            {
                _context.Specializations.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Specialization> Delete(int id)
        {
            var HospitalDb1 = await _context.Specializations.FindAsync(id);
            if (HospitalDb1 != null)
            {
                _context.Specializations.Remove(HospitalDb1);
                await _context.SaveChangesAsync();
                return HospitalDb1;
            }
            return null;
        }

        public IEnumerable<Specialization> GetAll()
        {
            var specializations = _context.Specializations.Select(x => new Specialization
            {
                id = x.id,
                SpecializationName = x.SpecializationName,
            }).ToList();
            return specializations;
        }

        public async Task<Specialization> GetById(int id)
        {
            var specializations = await _context.Specializations.Select(x => new Specialization
            {
                id = x.id,
                SpecializationName = x.SpecializationName,
            }).ToListAsync();
            var specialization = specializations.FirstOrDefault(x => x.id == id);
            if (specialization != null)
            {
                return specialization;
            }
            return null;
        }
    }
    }

