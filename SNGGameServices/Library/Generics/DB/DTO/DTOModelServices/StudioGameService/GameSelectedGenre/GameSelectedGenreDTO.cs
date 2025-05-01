using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre
{
    public class GameSelectedGenreDTO
    {
        public Guid Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "NumberOrder должено быть положительным числом")]
        public int NumberOrder { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "GenreId является обязательным")]
        public Guid GenreId { get; set; }
    }
}
