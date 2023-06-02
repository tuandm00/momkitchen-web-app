using AutoMapper;
using momkitchen.Mapper;
using momkitchen.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task ChangeStatus(int id, int status)
        {
            var result = await _ctx.SessionPackages.FindAsync(id);
            if(result != null)
            {
                //0 New 1Approve 2Reject 3Cancel
                if(result.Status == 0)
                {
                    if(status == 1)
                    {
                        result.Status = 1;
                    } else if(status == 2){
                        if (result.Status == 0) { result.Status = 2; }
                    } 
                }
                if(result.Status == 1)
                {
                    if(status == 3)
                    {
                        result.Status = 3;
                    }
                }
                if(result.Status == 2)
                {
                    if(status == 3)
                    {
                        result.Status= 3;
                    }
                }

                await _ctx.SaveChangesAsync();
            }

        }

        public List<SessionPackage> GetAllSessionPackage()
        {
            var result = _ctx.SessionPackages.Include(x => x.FoodPackage).ThenInclude(x => x.DishFoodPackages).ThenInclude(x => x.Dish).ToList();
            return result;
                
        }
    }
}
