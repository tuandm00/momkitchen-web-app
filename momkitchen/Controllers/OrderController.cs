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

        private readonly IOrderRepository _repository;
        private readonly IOrderService _service;

        public OrderController(IOrderRepository orderRepository, IOrderService orderService)
        {
            _repository = orderRepository;
            _service = orderService;
        }

        [HttpPost]
        public async Task CreateOrder(OrderDto orderDto)
        {
            try
            {
                await _repository.CreateOrder(orderDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        [Route("getorder")]
        public async Task<Order> GetOrderByID(int id)
        {
            return await _service.GetOrderByID(id);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrderbyid (int id)
        {
            return Ok(_repository.Test(id));
        }

        [HttpGet]
        [Route("getallorderdetail")]

        public List<Order> GetOrderDetailByOrderId() => _repository.GetOrderDetailByOrderId();

        [HttpGet]
        [Route("getpaymentbyorderid")]

        public Payment GetPaymentByOrderId(int orderid) => _repository.GetPaymentByOrderId(orderid);

        [HttpGet]
        [Route("getallorder")]

        public List<Order> GetAllOrder() => _repository.GetAllOrder();
    }
}
