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
    public class SessionController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;
        private readonly ISessionRepository _repository;
        private readonly IMapper _mapper;

        public SessionController(MomkitchenContext context,IMapper mapper , ISessionRepository repository)
        {
            _ctx = context;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Session> PostSession(SessionDto session)
        {
            try
            {
                return await _repository.CreateSession(session);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }
    }
}
