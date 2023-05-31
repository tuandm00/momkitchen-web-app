using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IFoodPackageInSessionRepository
    {
        Task ChangeStatus(int id, int status);

        List<SessionPackage> GetAllSessionPackage();
    }
}
