using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IOrderRepository
    {
       Task CreateOrder(OrderDto orderDto);

        Task<Order> GetOrderByID(int id);

        List<Order> GetOrderDetailByOrderId();

        Order Test(int id);

        Payment GetPaymentByOrderId(int orderid);

        List<Order> GetAllOrder();
    }
}
