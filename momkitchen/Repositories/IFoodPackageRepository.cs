using momkitchen.Models;
namespace momkitchen.Services


{
    public interface IFoodPackageRepository
    {
         List<SessionPackage> GetAllSessionPackage();
         List<DishFoodPackage> GetDishByFoodPackageId(int foodpackageid);

        FoodPackage GetAllFoodPackage(int foodpackageid);

        List<DishFoodPackage> GetAllDish(int dishid);

        SessionPackage GetAlllSessionPackageByFoodpackageId(int foodpackageid);

        void DeleteFoodPackage(int id);


    }
}
