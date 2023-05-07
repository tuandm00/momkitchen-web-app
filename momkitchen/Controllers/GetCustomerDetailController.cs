using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Models;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCustomerDetailController : ControllerBase
    {
        private readonly MomkitchenContext _context;

        public GetCustomerDetailController(MomkitchenContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getCustomer()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

    }
}
