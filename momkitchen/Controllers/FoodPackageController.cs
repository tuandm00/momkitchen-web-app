using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Models;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodPackageController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;

        public FoodPackageController(MomkitchenContext context)
        {

            _ctx = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetPackageFood()
        {
            List<FoodPackageInSession> foodPackageInSessions = await _ctx.FoodPackageInSessions.Select(f => new FoodPackageInSession()
            {
                Id = f.Id,
                FoodPackageId = f.FoodPackageId,
                Price = f.Price,
                Quantity = f.Quantity,
                RemainQuantity = f.RemainQuantity,
                Status = f.Status,
                CreateDate = DateTime.Now,
                FoodPackage = f.FoodPackage,
            }).ToListAsync();
            return Ok(foodPackageInSessions);
        }
    }
}
