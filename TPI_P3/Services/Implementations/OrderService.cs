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
                Id = orderDto.Id,
                UserId = orderDto.UserId,
                Status = orderDto.Status,
            };

            _context.Orders.Add(orderToAdd);
            _context.SaveChanges();
            return orderToAdd;
        }

        public OrderLine AddProductToOrderLine(OrderLineDto orderLineDto)
        {
            var product = _context.Products
                .Include(p => p.Colours)
                .Include(p => p.Sizes)
                .FirstOrDefault(p => p.ProductId == orderLineDto.ProductId);

            if (product != null)
            {

                var selectedColour = product.Colours.FirstOrDefault(c => c.Id == orderLineDto.ColourId);
                var selectedSize = product.Sizes.FirstOrDefault(s => s.Id == orderLineDto.SizeId);

                if (selectedColour == null || selectedSize == null)
                {

                    return null;
                }

                var orderLine = new OrderLine
                {
                    Product = product,
                    ProductId = product.ProductId,
                    ColourId = orderLineDto.ColourId,
                    SizeId = orderLineDto.SizeId,
                    Amount = orderLineDto.Amount,
                    OrderId = orderLineDto.OrderId,
                };

                _context.OrderLines.Add(orderLine);
                _context.SaveChanges();
                return orderLine;
            }


            return null;
        }


        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(p => p.OrderLines)
                .ThenInclude(ol => ol.Product)
                    .ThenInclude(p => p.Colours)
                .Include(p => p.OrderLines)
                .ThenInclude(ol => ol.Product)
                    .ThenInclude(p => p.Sizes)
                .ToList();
        }



        public Order? GetOrderById(int id)
        {
            return _context.Orders
                .Include(p => p.OrderLines)
                .ThenInclude(ol => ol.Product)
                    .ThenInclude(p => p.Colours)
                .Include(p => p.OrderLines)
                .ThenInclude(ol => ol.Product)
                    .ThenInclude(p => p.Sizes)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
