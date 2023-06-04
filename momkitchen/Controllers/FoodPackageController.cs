using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Models;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodPackageController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;
        private readonly IFoodPackageRepository _repository;

        public FoodPackageController(MomkitchenContext context, IFoodPackageRepository foodPackageRepository)
        {

            _ctx = context;
            _repository = foodPackageRepository;

        }

        [HttpGet]

        public List<SessionPackage> GetAllSessionPackage() => _repository.GetAllSessionPackage();
        
        [HttpGet]
        [Route("getdishbyfoodpackageid")]
         public List<DishFoodPackage> GetDishByFoodPackageId(int foodpackageid) => _repository.GetDishByFoodPackageId(foodpackageid);

        [HttpGet]
        [Route("getallfoodpackagebyid")]
        public FoodPackage GetAllFoodPackage(int foodpackageid) => _repository.GetAllFoodPackage(foodpackageid);

        [HttpGet]
        [Route("getalldishbyid")]
        public List<DishFoodPackage> GetAllDish(int dishid) => _repository.GetAllDish(dishid);

        [HttpGet]
        [Route("getallsessionpackagebyfoodpacakgeid")]
        public SessionPackage GetAlllSessionPackageByFoodpackageId(int foodpackageid) => _repository.GetAlllSessionPackageByFoodpackageId(foodpackageid); 

        [HttpDelete]
        [Route("deletefoodpackage")]

        public void DeleteFoodPackage(int id) => _repository.DeleteFoodPackage(id);
        
    }
}
