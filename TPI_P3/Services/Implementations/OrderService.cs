using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly TPIContext _context;
        public OrderService(TPIContext context)
        {
            _context = context;
        }

        public Order AddOrder(OrderDto orderDto)
        {
            var orderToAdd = new Order
            {
                UserId = orderDto.UserId,
                Status = orderDto.Status,
            };

            _context.Orders.Add(orderToAdd);
            _context.SaveChanges();
            return orderToAdd;
        }

        public OrderLine AddProductToOrderLine(OrderLineDto orderLineDto)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == orderLineDto.ProductId);

            if (product != null)
            {
                var orderLine = new OrderLine
                {
                    Product = product,
                    ProductId = product.ProductId,
                    Amount = orderLineDto.Amount,
                    OrderId = orderLineDto.OrderId,
                };

                _context.OrderLines.Add(orderLine);
                _context.SaveChanges();
                return orderLine;
            }

            // Manejar si el producto no se encuentra en la base de datos
            return null;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(p => p.OrderLines)
                .ThenInclude(ol => ol.Product) // Incluye los productos relacionados con las líneas de pedido
                .ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders
                .Include(p => p.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
