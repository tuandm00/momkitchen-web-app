using momkitchen.Models;

namespace momkitchen.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;

        public OrderService(IOrderRepository orderRepository)
        {
            repository = orderRepository;
        }
        public Task<Order> GetOrderByID(int id)
        {
            var result = repository.GetOrderByID(id);
            return result;
        }
    }
}
