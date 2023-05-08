using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface ISessionRepository
    {
            Task CreateSession(SessionDto session);
    }
}
