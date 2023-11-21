using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;
using TPI_P3.Services.Implementations;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly TPIContext _context;

        public UserController(IUserService service, TPIContext context)
        {
            _UserService = service;
            _context = context;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserDTO dto)
        {
            bool isUserNameExists = _context.Users.Any(u => u.UserName == dto.UserName);

            if (!isUserNameExists)
            {
                var newUser = new User()
                {
                    UserName = dto.UserName,
                    Name = dto.Name,
                    Password = dto.Password,
                    UserType = "Client"
                };
                return StatusCode(StatusCodes.Status201Created, _UserService.CreateUser(newUser));
            }
            return BadRequest($"Ya existe el userName '{dto.UserName}', ingrese otro");

            
        }

        [Authorize]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserDTO dto)
        {
            bool isUserNameExists = _context.Users.Any(u => u.UserName == dto.UserName);
            if (!isUserNameExists)
            {
                User userToUpdate = new User()
                {
                    UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                    UserName = dto.UserName,
                    Name = dto.Name,
                    Password = dto.Password,
                    UserType = "Client"
                };
                _UserService.UpdateUser(userToUpdate);
                return Ok("Usuario modificado exitosamente");
            }
            return BadRequest($"Ya existe el userName '{dto.UserName}', ingrese otro");

        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeletemyAccount()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            _UserService.DeleteUser(id);
            return NoContent();
        }
    }
}
