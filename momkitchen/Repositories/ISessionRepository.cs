using Microsoft.AspNetCore.Mvc;
using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface ISessionRepository
    {
        Task<SessionResponse> CreateSession(SessionDto session);

        Task<Session> DeleteSession(int id);

        Task UpdateStatusSession(int id, bool status);

        List<Session> GetAllSession();

        Task<SessionResponse> EnableOrDisableTime(int id, SessionDto session);

        SessionPackage GetAllSessionPackage(int id);

        List<SessionPackage> GetAllSessionPackage();

        List<SessionPackage> GetAllSessionPackageWithSessionStatusOn();

        void ChooseSessionForBatch(int batchid, int sessionid);

    }


}
