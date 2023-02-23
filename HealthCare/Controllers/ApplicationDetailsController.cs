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
    public class ApplicationDetailsController : ControllerBase
    {
        private readonly IRepositoryADR<ApplicationDetails> _repository2;
        public ApplicationDetailsController(IRepositoryADR<ApplicationDetails> repository2)
        {
            _repository2 = repository2;
        }
        [HttpGet]
        [Route("GetAllApplicationDetails")]
        public async Task<ActionResult<IEnumerable<ApplicationDetails>>> GetAll()
        {
            return await _repository2.GetAll();
        }

        [HttpGet]
        [Route("GetApplicationDetailById/{id}", Name = "GetApplicationDetailById")]
        public async Task<ActionResult<ApplicationDetails>> GetById(int id)
        {
            var applicationDetail = await _repository2.GetById(id);
            if (applicationDetail != null)
            {
                return Ok(applicationDetail);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ApplicationDetails applicationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            await _repository2.Create(applicationDetail);
            return CreatedAtRoute("GetApplicationDetailById", new { id = applicationDetail.ApplicationId }, applicationDetail);

        }

        [HttpPut]
        [Route("UpdateApplicationDetail/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ApplicationDetails applicationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository2.Update(id, applicationDetail);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Application Detail Not Found");
        }

        [HttpDelete("DeleteApplicationDetail/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository2.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Application Detail Not found with order detail id:{id}");
        }
    }
}
