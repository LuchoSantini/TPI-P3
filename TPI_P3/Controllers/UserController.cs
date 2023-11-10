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
        // Un usuario puede crear, updatearse - crear,ver ordenes y productos
        public UserController(IUserService service)
        {
            _UserService = service;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO user)
        {
            var newUser = new User()
            {
                UserName = user.UserName,
                Name = user.Name,
                Password = user.Password,
                UserType = "Client"
            };

            return StatusCode(StatusCodes.Status201Created,_UserService.CreateUser(newUser));
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDTO dto)
        {
            
            User userToUpdate = new User()
            {
                UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                UserName = User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value,
                Name = dto.Name,
                Password = dto.Password,
                UserType = "Client"
            };
            _UserService.UpdateUser(userToUpdate);
            return Ok();
        }
    }
}
