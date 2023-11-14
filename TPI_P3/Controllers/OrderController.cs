using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPI_P3.Data.Models;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet("GetOrderById/{id}")]
        public IActionResult GetOrderById(int id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderDto orderDto)
        {
            if(orderDto == null)
            {
                return BadRequest("La solicitud no es válida.");
            }
            _orderService.AddOrder(orderDto);
            return Ok(orderDto);
        }

        [HttpPost("AddProductToOrderLine")]
        public IActionResult AddProductToProductLine(OrderLineDto orderLineDto)
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
    }
}
