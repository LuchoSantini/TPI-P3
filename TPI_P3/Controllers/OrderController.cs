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
                //En caso de que falle para eso esta el !int.TryParse del if que devuelve el 400 generico
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

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    return BadRequest("No se pudo obtener o convertir el UserId a un valor entero.");
                }

                orderLineDto.UserId = userId;

                var addedOrderLine = _orderService.AddProductToOrderLine(orderLineDto);

                if (addedOrderLine == null)
                {
                    return NotFound("El producto no se encontró o no se pudo encontrar la Orden de productos.");
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
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null || !int.TryParse(userId, out int parsedUserId))
                {
                    return BadRequest("No se pudo obtener o convertir el UserId a un valor entero.");
                }

                return Ok(_orderService.GetAllOrders(parsedUserId));
            }

            return Forbid();

        }

    }
}
