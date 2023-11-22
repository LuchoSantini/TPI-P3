using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly TPIContext _context;
        public AdminController(IAdminService adminService, IUserService userService, TPIContext context)
        {
            _adminService = adminService;
            _userService = userService;
            _context = context;
        }

        
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            User userLogged = _userService.GetUserByUsername(User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value);
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin" && userLogged.Status)
            {
                return Ok(_adminService.GetUsers());
            }
            return Forbid();
        }

        [HttpPost("AddColour")]
        public IActionResult AddColourToTable([FromBody] string colour)
        {

            User userLogged = _userService.GetUserByUsername(User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value);
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin" && userLogged.Status)
            {
                bool existingColour = _adminService.CheckIfColourExists(colour);
                if(colour == "string" || String.IsNullOrEmpty(colour))
                {
                    return BadRequest("Ingrese un color");
                }
                if (!existingColour)
                {
                    var colourToAdd = new Colour()
                    {
                        ColourName = colour,
                    };
                    return StatusCode(StatusCodes.Status201Created, _adminService.AddColour(colour));
                }
                return BadRequest("El color ya existe");
            }
            return Forbid();
        }

        [HttpPost("AddSize")]
        public IActionResult AddSizeToTable([FromBody] string size)
        {
            User userLogged = _userService.GetUserByUsername(User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value);
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Admin" && userLogged.Status)
            {
                bool existingSize = _adminService.CheckIfSizeExists(size);
                if(size == "string" || String.IsNullOrEmpty(size))
                {
                    return BadRequest("Ingrese un talle");
                }

                if (!existingSize)
                {
                    var sizeToAdd = new Size()
                    {
                        SizeName = size,
                    };
                    return StatusCode(StatusCodes.Status201Created, _adminService.AddSize(size));
                }

                return BadRequest($"El talle {size} ya existe");
            }
            return Forbid();
        }
    }
}
