using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderByID(int id);
    }
}
