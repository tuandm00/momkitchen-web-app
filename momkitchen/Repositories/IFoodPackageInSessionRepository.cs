using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IFoodPackageInSessionRepository
    {
        Task ChangeStatus(int id, FoodPackageInSessionDto foodPackageInSessionDto);
    }
}
