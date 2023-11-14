using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;

namespace TPI_P3.Services.Interfaces
{
    public interface IOrderService
    {
        public OrderLine AddProductToOrderLine(OrderLineDto orderLineDto);
        public Order AddOrder(OrderDto orderDto);
        public List<Order> GetAllOrders();
        public Order? GetOrderById(int id);
    }
}
