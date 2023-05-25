using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IOrderRepository
    {
       Task CreateOrder(OrderDto orderDto);

        Task<Order> GetOrderByID(int id);

       // Task<Order> GetOrderDetailByOrderId(int id);

        Order Test(int id);
    }
}
