using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderController(MomkitchenContext context, IOrderRepository orderRepository, IMapper mapper)
        {
            _ctx = context;
            _repository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<OrderResponse> CreateOrder(OrderDto orderDto)
        {
            try
            {
               return await _repository.CreateOrder(orderDto);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
    }
}
