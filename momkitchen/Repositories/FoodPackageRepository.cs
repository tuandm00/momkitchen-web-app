using Microsoft.EntityFrameworkCore;
using momkitchen.Models;

namespace momkitchen.Services
{

    public class FoodPackageRepository : IFoodPackageRepository
    {
        DbSet<FoodPackage> _foodPackages;
        private readonly MomkitchenContext _ctx;

        public FoodPackageRepository(MomkitchenContext context)
        {
            _ctx = context;
            _foodPackages = _ctx.Set<FoodPackage>();
        }

        public List<SessionPackage> GetAllSessionPackage()
        {
            //return (FoodPackage)_foodPackages.Select(f => new FoodPackage()
            //{
            //    Id = f.Id,
            //    Name = f.Name,
            //    Image = f.Image,
            //    DefaultPrice = f.DefaultPrice,
            //    Chef = f.Chef,
            //    Description = f.Description,
            //    FoodPackageStyleId = f.FoodPackageStyleId,

            //});
            var result = _ctx.SessionPackages.Select(s => new SessionPackage()
            {
                Id = s.Id,
                FoodPackageId = s.FoodPackageId,
                SessionId = s.SessionId,
                Price = s.Price,
                Quantity = s.Quantity,
                RemainQuantity = s.RemainQuantity,
                Status = s.Status,
                CreateDate = s.CreateDate,
                FoodPackage = s.FoodPackage,
            });

            return result.ToList();
            
        }

        public  List<DishFoodPackage> GetDishByFoodPackageId(int foodpackageid)
        {

            var findfoodpackageid = _ctx.DishFoodPackages.Where(x => x.FoodPackageId == foodpackageid).Select(x => new DishFoodPackage()
            {
                DishId = x.DishId,
                FoodPackageId = x.FoodPackageId,
                Quantity = x.Quantity,
                DisplayIndex = x.DisplayIndex,
                Dish = x.Dish,
                FoodPackage = x.FoodPackage,

            });

            return findfoodpackageid.ToList();

        }

        public List<FoodPackage> GetAllFoodPackage(int foodpackageid)
        {
            var findpackageid = _ctx.FoodPackages.Where(x => x.Id == foodpackageid).Select(x => new FoodPackage()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                DefaultPrice = x.DefaultPrice,
                ChefId = x.ChefId,
                Description = x.Description,
                FoodPackageStyleId = x.FoodPackageStyleId,
            });

            return findpackageid.ToList();
        }

        public List<DishFoodPackage> GetAllDish(int dishid)
        {
            var finddishid = _ctx.DishFoodPackages.Where(x => x.FoodPackageId == dishid).Select(x => new DishFoodPackage()
            {
                DishId = x.DishId,
                FoodPackageId = x.FoodPackageId,
                Quantity = x.Quantity,
                DisplayIndex = x.DisplayIndex,
                Dish = x.Dish,
            });

            return finddishid.ToList();
        }
    }
}
