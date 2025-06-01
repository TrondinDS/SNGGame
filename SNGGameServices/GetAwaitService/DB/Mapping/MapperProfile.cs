using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;
using Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription;

namespace GetAwaitService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<JobCreateDTO, JobCreateDTOFront>().ReverseMap();
            CreateMap<UserSubscriptionCreateDTO, UserSubscriptionCreateDTOFront>().ReverseMap();


            CreateMap<UserReactionDTO, UserReactionCreateDTO>().ReverseMap();
            CreateMap<TopicDTO, TopicCreateDTO>().ReverseMap();
            CreateMap<CommentDTO, CommentCreateDTO>().ReverseMap();


            CreateMap<GameDTO, GameCreateDTO>().ReverseMap();
            CreateMap<GameLibraryDTO, GameLibraryCreateDTO>().ReverseMap();
            CreateMap<GameSelectedGenreDTO, GameSelectedGenreCreateDTO>().ReverseMap();
            CreateMap<GameSelectedTagDTO, GameSelectedTagCreateDTO>().ReverseMap();
            CreateMap<GenreDTO, GenreCreateDTO>().ReverseMap();
            CreateMap<StudioDTO, StudioCreateDTO>().ReverseMap();
            CreateMap<TagDTO, TagCreateDTO>().ReverseMap();
            CreateMap<BannedDTO, BannedCreateDTO>().ReverseMap();
            
        }
    }
}
