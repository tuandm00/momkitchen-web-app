using AutoMapper;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public class FoodPackageInSessionRepository : IFoodPackageInSessionRepository
    {
        private readonly MomkitchenContext _ctx;
        private readonly IMapper _mapper;

        public FoodPackageInSessionRepository(MomkitchenContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
        }

        public async Task ChangeStatus(int id, FoodPackageInSessionDto foodPackageInSessionDto)
        {
            var result = await _ctx.FoodPackageInSessions.FindAsync(id);
            if (result != null)
            {
                result.Status = foodPackageInSessionDto.Status;
                await _ctx.SaveChangesAsync();
            }
                

        }
    }
}
