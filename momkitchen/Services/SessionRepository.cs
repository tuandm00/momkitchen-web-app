using Microsoft.EntityFrameworkCore;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public class SessionRepository : ISessionRepository
    {
        private readonly MomkitchenContext _ctx;
        

        public SessionRepository(MomkitchenContext context)
        {
            _ctx = context;
        }

        public async Task CreateSession(SessionDto session)
        {
            var sessions = _ctx.Sessions.Select(s => new Session()
            {
                CreateDate = DateTime.Now,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Status = session.Status,
            });


            await _ctx.Sessions.AnyAsync();
            await _ctx.SaveChangesAsync();

        }
    }
}
