using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;
        private readonly ISessionRepository _repository;

        public SessionController(MomkitchenContext context, ISessionRepository repository)
        {
            _ctx = context;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> PostSession(SessionDto session)
        {
            await _repository.CreateSession(session);
            return Ok(session);
        }
    }
}
