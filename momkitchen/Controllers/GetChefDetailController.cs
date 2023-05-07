using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Models;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetChefDetailController : ControllerBase
    {

        private readonly MomkitchenContext _context;

        public GetChefDetailController(MomkitchenContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getChef()
        {
            return Ok(await _context.Chefs.ToListAsync());
        }

    }
}
