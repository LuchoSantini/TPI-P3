using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("AddOrder")]
        [Authorize]
        public IActionResult AddOrder([FromBody] OrderDto orderDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (role == "Client" || role == "Admin")
            {
                if (orderDto == null)
                {
                    return BadRequest("La solicitud no es válida.");
                }
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                //Checkeamos que el userId sea distinto de un int para parsearlo y llevarlo al service despues
                //En caso de que falle para eso esta el !int.TryParse que devuelve el 400 generico
                {
                    return BadRequest("No se pudo obtener o convertir el UserId a un valor entero.");
                }

                orderDto.UserId = userId;
                var orderToBeAdded = _orderService.AddOrder(orderDto);
                return Ok($"Orden agregada correctamente. ID: {orderToBeAdded.Id}");
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
