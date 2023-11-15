using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_P3.Data.Entities;
using TPI_P3.Services.Implementations;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        public AdminController(IAdminService adminService, IUserService userService)
        {
             _adminService = adminService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            User userLogged = _userService.GetUserByUsername(User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value);
            if (role == "Admin" && userLogged.Status)
            {
                return Ok(_adminService.GetUsers());
            }
            return Forbid();
        }
    }
}
