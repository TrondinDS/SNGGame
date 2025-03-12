using AutoMapper;
using BannedService.DB.Models;
using StudioGameService.DB.Model;
using UserService.DB.DTO.Banned;
using UserService.DB.DTO.Job;
using UserService.DB.DTO.User;
using UserService.DB.DTO.UserSubscription;
using UserService.DB.Models;

namespace UserService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserSubscription, UserSubscriptionDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Banned, BannedDTO>().ReverseMap();
        }
    }
}
