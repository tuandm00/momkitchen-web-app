using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IBatchRepository
    {
        List<Batch> GetAllBatch();
    }
}
