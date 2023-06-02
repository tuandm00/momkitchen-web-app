using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Mapper;
using momkitchen.Models;
using System.Collections.ObjectModel;
using System.Globalization;

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
        public static DateTime TranferDateTimeByTimeZone(DateTime dateTime, string timezoneArea)
        {

            ReadOnlyCollection<TimeZoneInfo> collection = TimeZoneInfo.GetSystemTimeZones();
            var timeZone = collection.ToList().Where(x => x.DisplayName.ToLower().Contains(timezoneArea)).First();

            var timeZoneLocal = TimeZoneInfo.Local;

            var utcDateTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneLocal, timeZone);

            return utcDateTime;
        }

        public static DateTime GetDateTimeTimeZoneVietNam()
        {

            return TranferDateTimeByTimeZone(DateTime.Now, "hanoi");
        }
        public static DateTime? StringToDateTimeVN(string dateStr)
        {

            var isValid = DateTime.TryParseExact(
                                dateStr,
                                "d'/'M'/'yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out var date
                            );
            return isValid ? date : null;
        }
        public async Task<SessionResponse> CreateSession(SessionDto session)
        {
            var newSession = new Session()
            {
                Id = session.Id,
                CreateDate = GetDateTimeTimeZoneVietNam(),
                StartTime = null,
                EndTime = null,
                Status = false,
                Title = session.Title,
            };

            await _ctx.Sessions.AddAsync(newSession);
            await _ctx.SaveChangesAsync();

            var mapped = _mapper.Map<SessionResponse>(newSession);

            mapped.CreateDate = newSession.CreateDate.Value.ToString("dd/MM/yyyy HH:mm");


            return mapped;

        }

        public async Task<SessionResponse> EnableOrDisableTime(int id, SessionDto session)
        {
            var result = await _ctx.Sessions.FindAsync(id);
            if (result != null && result.Status == false)
            {
                result.Id = id;
                result.StartTime = GetDateTimeTimeZoneVietNam();
                result.Status = true;
                await _ctx.SaveChangesAsync();
                var mapped = _mapper.Map<SessionResponse>(result);
                mapped.StartTime = result.StartTime.Value.ToString("dd/MM/yyyy HH:mm");
                return mapped;

            }
            else if (result != null && result.Status == true)
            {
                result.Id = id;
                result.EndTime = GetDateTimeTimeZoneVietNam();
                result.Status = false;
                await _ctx.SaveChangesAsync();
                var mapped = _mapper.Map<SessionResponse>(result);
                mapped.EndTime = result.EndTime.Value.ToString("dd/MM/yyyy HH:mm");
                return mapped;


            }

            return null;



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

        public async Task UpdateStatusSession(int id, bool status)
        {
            var result = await _ctx.Sessions.FindAsync(id);
            if (result != null)
            {
                result.Status = status;
                await _ctx.SaveChangesAsync();
            }

        }

        public List<Session> GetAllSession()
        {
            var sessions = _ctx.Sessions.Select(x => new Session()
            {
                Id = x.Id,
                CreateDate = x.CreateDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status,
                Title = x.Title,
            });
            return sessions.ToList();
        }

        public SessionPackage GetAllSessionPackage(int id)
        {
            var result = _ctx.SessionPackages.Where(x => x.Id == id).Select(x => new SessionPackage()
            {
                Id = id,
                FoodPackage = x.FoodPackage,
                Session = x.Session,
                Price = x.Price,
                Quantity = x.Quantity,
                RemainQuantity = x.RemainQuantity,
                Status = x.Status,
                CreateDate = x.CreateDate,
            }).FirstOrDefault();

            return result;
        }

        public List<SessionPackage> GetAllSessionPackageWithSessionStatusOn()
        {
            var sessionStatus = true;
            var sessionpackageStatus = 1;

            var sessionPackageList = _ctx.SessionPackages
                .Where(sp => sp.Status == sessionpackageStatus && _ctx.Sessions.Any(s => (s.Status ?? false) == sessionStatus && s.Id == sp.SessionId)).Select(x => new SessionPackage()
                {
                    Id = x.Id,
                    FoodPackage = x.FoodPackage,
                    Session = x.Session,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    RemainQuantity = x.RemainQuantity,
                    Status = x.Status,
                    CreateDate = x.CreateDate,
                })
                .ToList();

            return sessionPackageList;




        }

        public List<SessionPackage> GetAllSessionPackage()
        {
            var result = _ctx.SessionPackages.Select(x => new SessionPackage()
            {
                Id = x.Id,
                FoodPackage = x.FoodPackage,
                Session = x.Session,
                Price = x.Price,
                Quantity = x.Quantity,
                RemainQuantity = x.RemainQuantity,
                Status = x.Status,
                CreateDate = x.CreateDate,
            });

            return result.ToList();
        }

        public void ChooseSessionForBatch(int batchid, int sessionid)
        {
            var resultbatchid = _ctx.Batches.FirstOrDefault(x => x.Id == batchid);
            var resultsessionid = _ctx.Sessions.FirstOrDefault(x => x.Id == sessionid);

            resultbatchid.SessionId = resultsessionid.Id;

            _ctx.SaveChanges();
            

        }

        //public List<SessionPackage> GetAllSessionPackage()
        //{
        //    var result = _ctx.SessionPackages.Select(x => new SessionPackage() 
        //    {
        //    Id = x.Id,
        //    FoodPackage=x.FoodPackage,
        //    Session=x.Session,
        //    Price=x.Price,
        //    Quantity=x.Quantity,
        //    });
        //}
    }
}
