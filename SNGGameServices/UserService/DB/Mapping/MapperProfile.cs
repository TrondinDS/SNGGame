using AutoMapper;
using UserService.DB.DTO.User;
using UserService.DB.Models;

namespace UserService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
