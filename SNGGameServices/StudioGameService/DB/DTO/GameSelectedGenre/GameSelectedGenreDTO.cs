using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudioGameService.DB.Model;

namespace StudioGameService.DB.DTO.GameSelectedGenre
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
