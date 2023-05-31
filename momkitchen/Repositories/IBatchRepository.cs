using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IBatchRepository
    {
        Task<BatchReponse> CreateBatch(BatchDto batch);
        Task<Batch> GetBatchByShipper(int shipperId);
        List<Shipper> GetAllShipper();
        void AssignBatchForShipper(int batchid, string emailshipper);
        void ChooseShipperForBatch(int batchid, string emailshipper);
        List<Batch> GetAllBatch();
        Task<Batch> DeleteBatchById(int id);

    }
}
