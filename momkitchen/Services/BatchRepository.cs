using AutoMapper;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public class BatchRepository : IBatchRepository
    {
        private readonly MomkitchenContext _ctx;
        private readonly IMapper _mapper;

        public BatchRepository(MomkitchenContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
        }

        public async Task<BatchReponse> CreateBatch(BatchDto batch)
        {
            var newBatch = new Batch()
            {
                //status in Batch:  0 is for collect batch and 1 is for delivery batch
                ShipperId = batch.ShipperId,
                Status = batch.Status,
                SessionId = batch.SessionId,

            };
            await _ctx.Batches.AddAsync(newBatch);
            await _ctx.SaveChangesAsync();
            var mapped = _mapper.Map<BatchReponse>(newBatch);

            return mapped;
    }

    public List<Batch> GetAllBatch()
        {
            throw new NotImplementedException();
        }
    }
}
