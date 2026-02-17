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

            User user = new User
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Role = "default"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("login")]
        public ActionResult<UserDTO> Login([FromBody] UserDTO userDTO)
        {
            // check if user exist and role
            // if admin: redirect user to admin page in frontend
            // if default: redirect user to normal dashboard in frontend

            return userDTO;
        } 

    }
}
