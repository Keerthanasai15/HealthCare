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
    public class AppointmentsController : ControllerBase
    {
        private readonly IAPRepository<Appointment> _repository;

        public AppointmentsController(IAPRepository<Appointment> repository)
        {
            _repository = repository;

        }

        [HttpGet("GetAllAppointment")]
        public IEnumerable<Appointment> GetAppointment()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("GetAppointmentById/{id}", Name = "GetAppointmentById")]
        public async Task<ActionResult> GetAppointmentById(int id)
        {
            var appointment = await _repository.GetById(id);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound();
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(appointment);
            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, appointment);
        }

        [HttpPut("UpdateAppointment/{id}")]

        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, appointment);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Appointment not found");
        }

        [HttpDelete]
        [Route("DeleteAppointment/{id}", Name = "DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Appointment not found");
        }



    }
}
