using AutoMapper;
using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using StudioGameService.DB.Model;

namespace StudioGameService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<GameLibrary, GameLibraryDTO>().ReverseMap();
            CreateMap<GameSelectedGenre, GameSelectedGenreDTO>().ReverseMap();
            CreateMap<GameSelectedTag, GameSelectedTagDTO>().ReverseMap();
            CreateMap<Studio, StudioDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();


            CreateMap<Game, CardGameDTO>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id)) // Id -> GameId
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.RussianTitle)) // RussianTitle -> GameName
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.Id)) // ImageId -> GameId
                .ForMember(dest => dest.StudioName, opt => opt.MapFrom(src => src.Studio.Title)) // Studio.Title -> StudioName
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres != null
                                                ? src.Genres.Select(gsg => gsg.Genre).ToList() // Выбираем объекты Genre
                                                : new List<Genre>())); // Если Genres == null, возвращаем пустой список
                }
    }
}
