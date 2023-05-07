using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using momkitchen.Models;
using Microsoft.EntityFrameworkCore;


namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllDishController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;

        public GetAllDishController(MomkitchenContext context){
            _ctx = context;
        }

        [HttpGet]
        [Route("getalldish")]

        public async Task<IActionResult> getDish()
        {
            return Ok(await _ctx.Dishes.ToListAsync());
        }

    }
}
