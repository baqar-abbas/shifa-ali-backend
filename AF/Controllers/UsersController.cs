using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AF.Models;
using AF.Repositories;
using AF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AF.Controllers
{
    // UsersController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public UsersController(IUsersRepository usersRepository, PasswordService passwordService, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return Conflict("Email already in use.");
            }

            user.Password = _passwordService.HashPassword(user.Password);

            await _usersRepository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Fetch the user by email
            var user = await _usersRepository.GetUserByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verify password
            if (!_passwordService.VerifyPassword(user.Password, loginRequest.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate the JWT token
            var token = GenerateJwtToken(user);

            // Return the JWT token
            return Ok(new { Token = token });
        }

        // Method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.UTF8.GetBytes("your-secret-key");  // Replace with a strong key, store securely
            //var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]); // Fetch key from appsettings.json
            // var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); // Use _configuration to fetch the key
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),  // Token expires in 1 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _usersRepository.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usersRepository.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
