﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;
        private readonly IMapper _mapper;
        private readonly IBatchRepository _repository;

        public BatchController(MomkitchenContext context, IMapper mapper, IBatchRepository repository)
        {
            _ctx = context;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]

        public async Task<BatchReponse> CreateBatch(BatchDto batch)
        {
            try
            {
                return await _repository.CreateBatch(batch);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        [Route("assignbatchforshipper")]
        public  void AssignBatchForShipper(int batchid, string emailshipper)
        {
            _repository.AssignBatchForShipper(batchid, emailshipper);
        }

        [HttpPost]
        [Route("chooseshipperforbatch")]
        public void ChooseShipperForBatch(int batchid, string emailshipper)
        {
            _repository.ChooseShipperForBatch(batchid, emailshipper);
        }

        [HttpGet]

        public List<Shipper> GetAllShipper() => _repository.GetAllShipper();

        [HttpGet]
        [Route("getallbatch")]
         public List<Batch> GetAllBatch() => _repository.GetAllBatch();

        [HttpDelete]

        public async Task<Batch> DeleteBatchById(int batchid) => await _repository.DeleteBatchById(batchid);    
    }
}
