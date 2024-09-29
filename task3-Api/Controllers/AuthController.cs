using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using task3_Api.Models;
using task3_Api.Services;

namespace task3_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService; // Add this line

        // Modify the constructor to include TokenService
        public AuthController(SchoolContext context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService; // Add this line
        }

        [HttpPost("register/teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] SchoolUser user)
        {
            user.Role = "Teacher";
            return await RegisterUser(user);
        }

        [HttpPost("register/student")]
        public async Task<IActionResult> RegisterStudent([FromBody] SchoolUser user)
        {
            user.Role = "Student";
            return await RegisterUser(user);
        }

        private async Task<IActionResult> RegisterUser(SchoolUser user)
        {
            // Check for existing user
            if (await _context.SchoolUsers.AnyAsync(u => u.Email == user.Email))
            {
                return Conflict("User already exists.");
            }

            // Add user to the database
            _context.SchoolUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.SchoolUsers.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) return Unauthorized();

            // Here, validate the password (assuming a password field exists)
            // For example, you might want to use PasswordHasher

            var token = _tokenService.GenerateToken(user); // Replace GenerateToken with _tokenService.GenerateToken(user)
            return Ok(new { Token = token });
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; } // Add password field for login
    }
}
