using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IOrderRepository
    {
       Task<OrderResponse> CreateOrder(OrderDto orderDto);
    }
}
