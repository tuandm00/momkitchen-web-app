    using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                //status in Batch:  0(False) is for collect batch and 1(True) is for delivery batch
                Status = batch.Status,
                SessionId = batch.SessionId,

            };
            await _ctx.Batches.AddAsync(newBatch);
            await _ctx.SaveChangesAsync();
            var mapped = _mapper.Map<BatchReponse>(newBatch);

            return mapped;
        }

        public async Task<Batch> GetBatchByShipper(int shipperId)
        {
            return await _ctx.Batches.Include(x => x.Orders).Where(x => x.ShipperId == shipperId).FirstOrDefaultAsync();

        }

        public List<Shipper> GetAllShipper()
        {
            var result = _ctx.Shippers.Select(x => new Shipper()
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                BatchId = x.BatchId,
                Image = x.Image,
            });

            return result.ToList();
        }

        public void AssignBatchForShipper(int batchid, string emailshipper)
        {
            //    var result = GetAllShipper();
            //    result = (List<Shipper>)_ctx.Shippers.Select(x => new Shipper()
            //    {
            //        if (x.Email == emailshipper) {
            //        x.BatchId = batchid;
            //    }
            //});

            var shipper = _ctx.Shippers.ToList();
            foreach (var s in shipper)
            {
                if (s.Email.Equals(emailshipper))
                {
                    s.BatchId = batchid;
                }
            }   
            _ctx.SaveChanges();

        }

        public void ChooseShipperForBatch(int batchid, string emailshipper)
        {
            var shipperid = _ctx.Shippers.Where(x => x.Email == emailshipper).Select(x => x.Id).FirstOrDefault();
            var batch = _ctx.Batches.ToList();
            foreach (var b in batch)
            {
                if (b.Id == batchid)
                {
                    AssignBatchForShipper(batchid, emailshipper);
                    b.ShipperId = shipperid;
                }
            }
            _ctx.SaveChanges();

        }

        public List<Batch> GetAllBatch()
        {
            var resul = _ctx.Batches.Select(x => new Batch()
            {
                Id = x.Id,
                Shipper = x.Shipper,
                Status = x.Status,
                Session = x.Session,
            });

            return resul.ToList();
        }

        public async Task<Batch> DeleteBatchById(int batchid)
        {
            var result = await _ctx.Batches.FindAsync(batchid);

            _ctx.Batches.Remove(result);
            _ctx.SaveChanges();

            return result;
        }
    }
}






