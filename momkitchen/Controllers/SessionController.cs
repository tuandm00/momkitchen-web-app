using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly MomkitchenContext _ctx;
        private readonly ISessionRepository _repository;
        private readonly IMapper _mapper;

        public SessionController(MomkitchenContext context,IMapper mapper , ISessionRepository repository)
        {
            _ctx = context;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<SessionResponse> PostSession(SessionDto session)
        {
            try
            {
                return await _repository.CreateSession(session);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
            }

        [HttpDelete("{id}")]
        public async Task<Session> DeleteSession(int id)
        {
            try
            {
                return await _repository.DeleteSession(id);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> PatchSession(int id, bool status)
        {
            await _repository.UpdateStatusSession(id, status);
            return Ok();
        }

        [HttpGet]

        public List<Session> GetAllSession() => _repository.GetAllSession();

        [HttpPut]
        [Route("enablestarttime")]
        public async Task<SessionResponse> EnableStartTime(int id, SessionDto session)
        {
            try
            {
                 return await _repository.EnableOrDisableTime(id, session);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        [HttpGet]
        [Route("getallsessionpackagebyid")]

        public SessionPackage GetAllSessionPackage(int id) => _repository.GetAllSessionPackage(id);

        [HttpGet]
        [Route("getallsessionpackagewithsessionstatuson")]
        public List<SessionPackage> GetAllSessionPackageWithSessionStatusOn() => _repository.GetAllSessionPackageWithSessionStatusOn();  
        
        [HttpGet]
        [Route("getallsessionpackage")]

        public List<SessionPackage> GetAllSessionPackage() => _repository.GetAllSessionPackage();

        [HttpGet]
        [Route("choosesessionforbatch")]

        public void ChooseSessionForBatch(int batchid, int sessionid) => _repository.ChooseSessionForBatch(batchid, sessionid); 
    }
}
