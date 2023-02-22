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
    public class AdminsController : Controller
    {
        private readonly IPRepository<Patient> _repository2;
        private readonly IARepository<Admin> _repository3;
        private readonly IRepositoryDoctor<Doctor> _repository6;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AdminsController(IARepository<Admin> repository, IPRepository<Patient> repository2, IRepositoryDoctor<Doctor> repository6, ApplicationDbContext context, IConfiguration configuration)
        {

            _repository3 = repository;
            _repository2 = repository2;
            _repository6 = repository6;
            _context = context;
            _configuration = configuration;
        }

        //Admin CRUD operations
        [HttpGet]
        [Route("GetAllAdmin")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            return await _repository3.GetAll();

        }

        [HttpGet]
        [Route("GetAdminById/{id}", Name = "GetAdminById")]
        public async Task<ActionResult<Admin>> GetById(int id)
        {
            var admin = await _repository3.GetById(id);
            if (admin != null)
            {
                return Ok(admin);
            }
            return NotFound();

        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> Create([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository3.Create(admin);
            return CreatedAtRoute("GetAdminById", new { id = admin.Id }, admin);
        }


        [HttpPut]
        [Route("UpdateAdmin/{id}")]
        public async Task<IActionResult> Update(int id, Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository3.Update(id, admin);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Admin Not Found");
        }

        [HttpDelete]
        [Route("DeleteAdmin/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository3.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Admin not found with this id");

        }

        //Admin Login using Jwt token

        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] AdminLoginModel adminLogin)
        {
            var currentAdmin = _context.Admin.FirstOrDefault(x => x.UserName == adminLogin.UserName && x.Password == adminLogin.Password);
            if (currentAdmin == null)
            {
                return NotFound("Invalid Username or password");
            }
            var token = GenreateToken(currentAdmin);
            if (token == null)
            {
                return NotFound("Invalid Credentials");
            }
            return Ok(token);

        }


        [NonAction]
        public string GenreateToken(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var myClaims = new List<Claim>
            {

                new Claim(ClaimTypes.Name,admin.UserName),
                new Claim(ClaimTypes.Email,admin.Email),
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

