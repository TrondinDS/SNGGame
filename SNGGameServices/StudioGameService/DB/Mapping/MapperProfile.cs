using AutoMapper;
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
        }
    }
}
