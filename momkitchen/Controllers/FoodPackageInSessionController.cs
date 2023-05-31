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
    public class FoodPackageInSessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MomkitchenContext ctx;
        private readonly IFoodPackageInSessionRepository _repository;

        public FoodPackageInSessionController(MomkitchenContext context, IMapper mapper, IFoodPackageInSessionRepository repository)
        {
            ctx = context;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPatch]

        public async Task ChangeStatus(int id, int status)
        {
            try
            {
                 await _repository.ChangeStatus(id, status);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet]
        public List<SessionPackage> GetAllSessionPackage() => _repository.GetAllSessionPackage();
    }
}
