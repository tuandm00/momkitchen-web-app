using momkitchen.Models;
namespace momkitchen.Services


{
    public interface IFoodPackageRepository
    {
         List<SessionPackage> GetAllSessionPackage();
         List<DishFoodPackage> GetDishByFoodPackageId(int foodpackageid);

        List<FoodPackage> GetAllFoodPackage(int foodpackageid);

        List<DishFoodPackage> GetAllDish(int dishid);
    }
}
