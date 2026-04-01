using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;
using webapi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] UserDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Email))
                return BadRequest("Email is required");

            if (string.IsNullOrWhiteSpace(userDTO.Password) || userDTO.Password.Length < 6)
                return BadRequest("Password is required and must be at least 6 characters long");

            var existingUser = await _userRepository.GetByEmailAsync(userDTO.Email);
            if (existingUser != null) 
                return Conflict("Email already exists");

            User user = new()
            {
                Email = userDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                Role = "default"
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            Console.WriteLine($"User created: {user.Email}");
            return Ok(new { user.Email });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO userDTO)
        {
            var user = await _userRepository.GetByEmailAsync(userDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password))
                return Unauthorized("Wrong email or password");

            var token = GenerateJwtToken(user);

            return Ok(new { token }); // ✅ Only return token
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_LONG_SECRET_KEY_FOR_AUTH_2026"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}