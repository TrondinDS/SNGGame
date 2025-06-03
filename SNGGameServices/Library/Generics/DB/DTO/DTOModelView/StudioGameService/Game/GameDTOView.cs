using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game
{
    public class GameDTOView
    {
        public GameDTO Game { get; set; }
        public StatisticGameDTO? StatisticGame {get; set;}
        public IEnumerable<TagDTO>? SelectedTags { get; set; }
        public IEnumerable<GenreDTO>? SelectedGenres { get; set; }
    }
}
