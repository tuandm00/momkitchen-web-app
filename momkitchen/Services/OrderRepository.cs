using AutoMapper;
using Microsoft.EntityFrameworkCore;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public class OrderRepository : IOrderRepository
    {
       
            private readonly MomkitchenContext _ctx;
            private readonly IMapper _mapper;

            public OrderRepository(MomkitchenContext context, IMapper mapper)
            {
                _ctx = context;
                _mapper = mapper;
            }
            public async Task<OrderResponse> CreateOrder(OrderDto orderDto)
            {
            var order = new Order()
            {
                Status = "New",
                DeliveryStatus = "New",
                Quantity = orderDto.Quantity, 
            };

            var createdOrder = await _ctx.Orders.AddAsync(order);
            await _ctx.SaveChangesAsync();

            var mapped = _mapper.Map<OrderResponse>(createdOrder.Entity);


            return mapped;
            }
        
    }
}
