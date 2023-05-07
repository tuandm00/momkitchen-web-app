using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchAccountController : ControllerBase
    {
        private readonly IAccountRepository userrepo;

        public SearchAccountController(IAccountRepository userRepository)
        {
            userrepo = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAccount(string email, int page = 1)
        {
            try
            {
                var result = userrepo.GetAll(email, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Nothing to search");
            }
        }
    }
}
