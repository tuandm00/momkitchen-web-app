using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IBatchRepository
    {
        List<Batch> GetAllBatch();

        Task<BatchReponse> CreateBatch(BatchDto batch);
    }
}
