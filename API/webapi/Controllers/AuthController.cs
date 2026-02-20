using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }
        [HttpPost("create")]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO userDTO)
        {
          
            var exists = await _db.Users.AnyAsync(u => u.Email == userDTO.Email);

            if (exists) 
            {
                return Conflict("Email already exists");
            }

            User user = new()
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Role = "default"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(new { user.Email });
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO userDTO)
        {

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == userDTO.Email && u.Password == userDTO.Password);

            if (user == null)
            {
                return Unauthorized("Wrong email or password");
            }

            return Ok(new { user.Id, user.Email, user.Role });
        }

    }
}
