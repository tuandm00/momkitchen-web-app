using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface ISessionRepositoryClass
    {
            Task CreateSession(SessionDto session);
    }
}
