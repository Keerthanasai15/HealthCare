using HealthCare.IRepository;
using HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.IRepository.IRepository;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPRepository<Patient> _iPRepository;
        private readonly ApplicationDbContext _context;

        public PatientsController(IPRepository<Patient> iPRepository, IConfiguration configuration, ApplicationDbContext context)
        {
            _iPRepository = iPRepository;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        [Route("GetAllPatients")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            return await _iPRepository.GetAll();
        }

        [HttpGet]
        [Route("GetPatientById/{id}", Name = "GetPatientById")]
        public async Task<ActionResult<Patient>> GetById(int id)
        {
            var patient= await _iPRepository.GetById(id);
            if (patient != null)
            {
                return Ok(patient);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("SearchByName/{Name}")]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchByName(string Name)
        {
            if (Name == null)
            {
                var a = await _iPRepository.GetAll();
                return a;
            }

            return await _iPRepository.SearchByName(Name);

        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            await _iPRepository.Create(patient);
            return CreatedAtRoute("GetPatientById", new { id = patient.PatientId }, patient);

        }


        [HttpPut]
        [Route("UpdatePatient/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _iPRepository.Update(id, patient);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Patient Not Found");
        }

        [HttpDelete("DeletePatient/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _iPRepository.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Patient Not found with Patient id:{id}");
        }

        //Login using JWt

        [HttpPost]
        [Route("Login")]

        public ActionResult Login([FromBody] PatientLoginModel patientLoginModel)
        {
            var currentPatient = _context.Patients.FirstOrDefault(x => x.UserName == patientLoginModel.UserName && x.PatientPassword == patientLoginModel.PatientPassword);
            if (currentPatient == null)
            {
                return NotFound("Invalid UserName or Password");

            }
            var token = GenerateToken(currentPatient);
            if (token == null)
            {
                return NotFound("Invalid Credentials");
            }
            return Ok(token);
        }

        [NonAction]
        public string GenerateToken(Patient patient)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var myClaims = new List<Claim>
            {

                new Claim(ClaimTypes.Name,patient.UserName),
                new Claim(ClaimTypes.Email,patient.PatientEmail),
                new Claim(ClaimTypes.MobilePhone,patient.PatientPhNo)

            };
            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
                                             audience: _configuration["JWT:audience"],
                                             claims: myClaims,
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);




        }





    }
}

    

