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
    public class DoctorsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryDoctor<Doctor> _repository;
        public DoctorsController(IRepositoryDoctor<Doctor> repository, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _context = dbContext;
        }

        [HttpGet]
        [Route("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("GetDoctorById/{id}", Name = "GetDoctorById")]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            var doctor= await _repository.GetById(id);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            await _repository.Create(doctor);
            return CreatedAtRoute("GetDoctorById", new { id = doctor.DoctorId }, doctor);

        }
        [HttpPut]
        [Route("UpdateDoctor/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if (result != null)
            {

                return NoContent();
            }
            return NotFound("Doctor Not Found");
        }

        [HttpDelete("DeleteDoctor")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Doctor Not found with doctor id:{id}");
        }

        //Login For Employee using Jwt

        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] DoctorLoginModel doctorLoginModel)
        {
            var currentDoctor = _context.Doctors.FirstOrDefault(x => x.UserName == doctorLoginModel.UserName && x.Password == doctorLoginModel.Password);
            if (currentDoctor == null)
            {
                return NotFound("Invalid UserName or Password");
            }
            var token = GenerateToken(currentDoctor);
            if (token == null)
            {
                return NotFound("Invalid Credentials");
            }
            token += currentDoctor.IsApproved.ToString();
            return Ok(token);
        }

        [NonAction]
        public string GenerateToken(Doctor doctor)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var myClaims = new List<Claim>
            {

                new Claim(ClaimTypes.Name,doctor.UserName),
                new Claim(ClaimTypes.Email,doctor.DoctorEmail),
                new Claim(ClaimTypes.MobilePhone,doctor.DoctorPhNo)

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

