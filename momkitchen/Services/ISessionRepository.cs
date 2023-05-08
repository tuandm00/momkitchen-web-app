using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface ISessionRepository
    {
            Task<Session> CreateSession(SessionDto session);
    }
}
