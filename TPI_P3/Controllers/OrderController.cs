using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_P3.Data.Models;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(OrderDto orderDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Client") // ver si agregamos otro role
            {
                if (orderDto == null)
                {
                    return BadRequest("La solicitud no es válida.");
                }
                _orderService.AddOrder(orderDto);
                return Ok(orderDto);
            }
            return Forbid();
        }

        [HttpPost]
        [Route("AddProductToOrderLine")]
        public IActionResult AddProductToProductLine(OrderLineDto orderLineDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin")
            {
                if (orderLineDto == null)
                {
                    return BadRequest("La solicitud no es válida.");
                }

                var addedOrderLine = _orderService.AddProductToOrderLine(orderLineDto);

                if (addedOrderLine == null)
                {
                    return NotFound("El producto no se encontró o no se pudo agregar a la línea de productos.");
                }

                return Ok(addedOrderLine);
            }
            return Forbid();

        }

        [HttpGet]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Client" || role == "Admin")
            {
                return Ok(_orderService.GetAllOrders());
            }
            return Forbid();

        }

        [HttpGet]
        [Route("GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Client" || role == "Admin")
            {
                return Ok(_orderService.GetOrderById(id));
            }
            return Forbid();
        }




    }
}
