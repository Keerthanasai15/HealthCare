using HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISRepository<Specialization> _repository;
        public SpecializationController(ISRepository<Specialization> repository)
        {
            _repository = repository;
        }
        [HttpGet("GetAllSpecialization")]

        public IEnumerable<Specialization> GetSpecialization()
        {
            return _repository.GetAll();
        }
        [HttpGet]
        [Route("GetSpecializationById/{id}", Name = "GetSpecializationById")]

        public async Task<ActionResult> GetSpecializationById(int id)
        {
            var specialization = await _repository.GetById(id);
            if (specialization != null)
            {
                return Ok(specialization);
            }
            return NotFound();
        }

        [HttpPost("CreateSpecialization")]
        public async Task<IActionResult> CreateSpecialization([FromBody] Specialization specialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(specialization);
            return CreatedAtRoute("GetSpecializationById", new { id = specialization.id }, specialization);
        }
        [HttpDelete]
        [Route("DeleteSpecialization/{id}", Name = "DeleteSpecialization")]
        public async Task<IActionResult> DeleteSpecializaton(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Specialization not found");
        }
    }


}

