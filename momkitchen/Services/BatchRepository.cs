using momkitchen.Models;

namespace momkitchen.Services
{
    public class BatchRepository : IBatchRepository
    {
        private readonly MomkitchenContext _ctx;

        public BatchRepository(MomkitchenContext context)
        {
            _ctx = context;
        }

        public List<Batch> GetAllBatch()
        {
            throw new NotImplementedException();
        }
    }
}
