using AutoMapper;
using StudioGameService.DB.DTO.Game;
using StudioGameService.DB.DTO.GameLibrary;
using StudioGameService.DB.DTO.GameSelectedGenre;
using StudioGameService.DB.DTO.GameSelectedTag;
using StudioGameService.DB.DTO.Genre;
using StudioGameService.DB.DTO.Studio;
using StudioGameService.DB.DTO.Tag;
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
        }
    }
}
