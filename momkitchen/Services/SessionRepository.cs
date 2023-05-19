using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public class SessionRepository : ISessionRepository
    {
        private readonly MomkitchenContext _ctx;
        private readonly IMapper _mapper;


        public SessionRepository(MomkitchenContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
        }

        public async Task<SessionResponse> CreateSession(SessionDto session)
        {
            var newSession = new Session()
            {
                Id = session.Id,
                CreateDate = DateTime.Now,
                StartTime = null,
                EndTime = null,
                Status = false,
                Title = session.Title,
            };

            await _ctx.Sessions.AddAsync(newSession);
            await _ctx.SaveChangesAsync();

            var mapped = _mapper.Map<SessionResponse>(newSession);

            mapped.CreateDate = newSession.CreateDate.Value.ToString("dd/MM/yyyy");
            

            return mapped;

        }

        public async Task<SessionResponse> EnableOrDisableTime(int id, SessionDto session)
        {
            var result = await _ctx.Sessions.FindAsync(id);
            if (result != null && result.Status == false)
            {
                result.Id = id;
                result.StartTime = DateTime.Now;
                result.Status = true;
                await _ctx.SaveChangesAsync();


            }
            else if(result != null && result.Status == true)
            {
                result.Id = id;
                result.EndTime = DateTime.Now;
                result.Status = false;
                await _ctx.SaveChangesAsync();

            }

            var mapped = _mapper.Map<SessionResponse>(result);
            mapped.StartTime = result.StartTime.Value.ToString("dd/MM/yyyy HH:mm");
            mapped.EndTime = result.EndTime.Value.ToString("dd/MM/yyyy HH:mm");


            return mapped;

            

        }

        public async Task<Session> DeleteSession(int id)
        {
            //if(_ctx.Sessions == null)
            //{
            //    return "not found";
            //}

            var findId = await _ctx.Sessions.FindAsync(id);
            //if (findId == null)
            //{
            //    return NotFound();
            //}
            _ctx.Sessions.Remove(findId);
            await _ctx.SaveChangesAsync();

            return findId;
        }

        public async Task UpdateStatusSession(int id, bool  status)
        {
            var result = await _ctx.Sessions.FindAsync(id);
            if(result != null)
            {
                result.Status = status;
                await _ctx.SaveChangesAsync();
            }

        }

        public List<Session> GetAllSession()
        {
            var sessions = _ctx.Sessions.Select(x => new Session() { 
                Id = x.Id,
                CreateDate = x.CreateDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status,
                Title = x.Title,
            });
            return sessions.ToList();
        }
    }
}
