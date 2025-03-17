using AutoMapper;
using UserActivityService.DB.DTO.Comment;
using UserActivityService.DB.DTO.Topic;
using UserActivityService.DB.DTO.UserReaction;
using UserActivityService.DB.Models;

namespace UserActivityService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Topic, TopicDTO>().ReverseMap();
            CreateMap<UserReaction, UserReactionDTO>().ReverseMap();
        }
    }
}
