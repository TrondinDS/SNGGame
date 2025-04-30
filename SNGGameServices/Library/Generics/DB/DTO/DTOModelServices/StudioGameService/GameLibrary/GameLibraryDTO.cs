using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary
{
    public class GameLibraryDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId является обязательным")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Дата является обязательной")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Статус является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "Статус должен быть положительным числом")]
        public int Status { get; set; }

        [Range(1, 10, ErrorMessage = "Рейтинг должен быть от 1 до 10")]
        public int Rating { get; set; }

        public bool IsBought { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GameId должен быть положительным числом")]
        public int GameId { get; set; }
    }
}
