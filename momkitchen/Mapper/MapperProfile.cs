using AutoMapper;
using momkitchen.Models;

namespace momkitchen.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, Account>().ReverseMap();
            CreateMap<RegisterDto, Account>().ReverseMap();
            CreateMap<SessionResponse, Session>().ReverseMap();
            CreateMap<SessionResponse, SessionDto>().ReverseMap();
        }
    }
}
