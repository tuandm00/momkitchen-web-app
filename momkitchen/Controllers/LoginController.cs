using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MomkitchenContext _ctx;
        private readonly IAccountRepository _repository;

        public LoginController(IConfiguration configuration, MomkitchenContext context, IAccountRepository repository)
        {
            _configuration = configuration;
            _ctx = context;
            _repository = repository;
        }

        [HttpPost]
        public AuthenticationResult Login(LoginDto account)
        {
            try
            {
                return _repository.Login(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }




    }
    }

