using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;
using Library.Generics.DB.DTO.DTOModelView.UserActivityService.Topic;
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

            CreateMap<Topic, TopicDTOView>()
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.CommentDTOs, opt => opt.MapFrom(src => src.Comments));
        }
    }
}
