using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre
{
    public class GameSelectedGenreDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public Guid Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "NumberOrder должено быть положительным числом")]
        public int NumberOrder { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GameId должен быть положительным числом")]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "GenreId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GenreId должен быть положительным числом")]
        public Guid GenreId { get; set; }
    }
}
