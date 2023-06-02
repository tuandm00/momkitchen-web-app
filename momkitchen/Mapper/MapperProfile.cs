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
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<Batch, BatchReponse>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<GetAllUserDto, Account>().ReverseMap();
        }
    }
}
