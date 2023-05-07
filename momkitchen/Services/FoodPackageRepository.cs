using Microsoft.EntityFrameworkCore;
using momkitchen.Models;

namespace momkitchen.Services
{
    public class FoodPackageRepository : IFoodPackageRepository
    {
        private readonly MomkitchenContext _ctx;

        public FoodPackageRepository(MomkitchenContext context)
        {
            _ctx = context;
        }

        
        
        
    }
}
