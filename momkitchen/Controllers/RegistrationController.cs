using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;
using System.Data;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public RegistrationController(IConfiguration configuration, IAccountRepository repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDto account)
        {
            await _repository.Register(_mapper.Map<Account>(account));
            return Ok();
        }
    }
}


