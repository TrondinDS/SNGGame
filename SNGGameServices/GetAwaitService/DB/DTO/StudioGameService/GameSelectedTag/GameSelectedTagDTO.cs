using System.ComponentModel.DataAnnotations;

namespace GetAwaitService.DB.DTO.StudioGameService.GameSelectedTag
{
    public class GameSelectedTagDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GameId должен быть положительным числом")]
        public int GameId { get; set; }

        [Required(ErrorMessage = "TagId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "TagId должен быть положительным числом")]
        public int TagId { get; set; }
    }
}
