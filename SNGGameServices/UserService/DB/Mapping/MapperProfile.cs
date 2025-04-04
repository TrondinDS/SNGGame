using AutoMapper;
using BannedService.DB.Models;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription;
using StudioGameService.DB.Model;

using UserService.DB.Models;

namespace UserService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<UserSubscription, UserSubscriptionDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Banned, BannedDTO>().ReverseMap();
        }
    }
}
