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

        //[HttpPost("AddColour")]
        //public IActionResult AddColourToTable(string colour)
        //{
        //    Response response = new Response();

        //    if (response.Message == "Error al agregar el color")
        //    {
        //        return BadRequest("No se pudo agregar el color");
        //    }
        //    else if (colour == "string" || string.IsNullOrEmpty(colour))
        //    {
        //        return BadRequest("Por favor ingrese un color");
        //    }
        //    return Ok(_adminService.AddColour(colour));
        //}

        [HttpPost("AddColour")]
        public IActionResult AddColourToTable(string colour)
        {
            bool exsitingColour = _context.Colours.Any(u => u.ColourName == colour);

            User userLogged = _userService.GetUserByUsername(User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value);
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin" && userLogged.Status)
            {
                if (!exsitingColour)
                {
                    var colourToAdd = new Colour()
                    {
                        ColourName = colour,
                    };
                    return StatusCode(StatusCodes.Status201Created, _adminService.AddColour(colour));
                }
                return BadRequest("No se pudo agregar el color");
            }
            return Forbid();
        }

        [HttpPost("AddSize")]
        public IActionResult AddSizeToTable(string size)
        {
            bool exsitingSize = _context.Sizes.Any(u => u.SizeName == size);

            User userLogged = _userService.GetUserByUsername(User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value);
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Admin" && userLogged.Status)
            {
                if (!exsitingSize)
                {
                    var sizeToAdd = new Size()
                    {
                        SizeName = size,
                    };
                    return StatusCode(StatusCodes.Status201Created, _adminService.AddSize(size));
                }
                return BadRequest("No se pudo agregar el talle");
            }
            return Forbid();  
        }
    }
}
