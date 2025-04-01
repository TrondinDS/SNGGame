using System.ComponentModel.DataAnnotations;

namespace GetAwaitService.DB.DTO.StudioGameService.GameSelectedGenre
{
    public class GameSelectedGenreDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "NumberOrder должено быть положительным числом")]
        public int NumberOrder { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GameId должен быть положительным числом")]
        public int GameId { get; set; }

        [Required(ErrorMessage = "GenreId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GenreId должен быть положительным числом")]
        public int GenreId { get; set; }
    }
}
