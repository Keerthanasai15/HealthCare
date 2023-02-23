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
    public class AppointmentStatusController : ControllerBase
    {
        private readonly IRepositoryStatus<AppointmentStatus> _repository;

        public AppointmentStatusController(IRepositoryStatus<AppointmentStatus> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllStatus")]
        public async Task<ActionResult<IEnumerable<AppointmentStatus>>> GetAll()
        {
            return await _repository.GetAll();
        }
        [HttpGet]
        [Route("GetstatusById/{id}", Name = "GetstatusById")]
        public async Task<ActionResult<AppointmentStatus>> GetCityById(int id)
        {
            var status = await _repository.GetById(id);
            if (status != null)
            {
                return Ok(status);
            }
            return NotFound();

        }
        [HttpPost]
        [Route("CreateStatus")]
        public async Task<IActionResult> Create([FromBody] AppointmentStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            await _repository.Create(status);
            return Ok();
           // return CreatedAtRoute("GetCitiById", new { id = status.StatusId }, status);
        }

        [HttpPut]
        [Route("UpdateStatus/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, status);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("status Not Found");
        }

        [HttpDelete("DeleteStatus")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"status Not found with id:{id}");
        }

    }
}
