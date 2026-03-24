using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;
using webapi.Models;

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
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Email))
            {
                return BadRequest("Email is required");
            }

            if (string.IsNullOrWhiteSpace(userDTO.Password) || userDTO.Password.Length < 6)
            {
                return BadRequest("Password is required and must be at least 6 characters long");
            }

            var existingUser = await _userRepository.GetByEmailAsync(userDTO.Email);

            if (existingUser != null) 
            {
                return Conflict("Email already exists");
            }

            User user = new()
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Role = "default"
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            Console.WriteLine($"User created: {user.Email}");
            return Ok(new { user.Email });
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO userDTO)
        {

            var user = await _userRepository.GetByEmailAsync(userDTO.Email);

            if (user == null || user.Password != userDTO.Password)
            {
                return Unauthorized("Wrong email or password");
            }

            Console.WriteLine($"User logged in: {user.Email}");
            return Ok(new { user.Id, user.Email, user.Role });
        }

    }
}
