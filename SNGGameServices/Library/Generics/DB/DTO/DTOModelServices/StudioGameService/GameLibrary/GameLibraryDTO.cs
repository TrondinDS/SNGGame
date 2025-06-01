using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary
{
    public class GameLibraryDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "UserId является обязательным")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Дата является обязательной")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Статус является обязательным")]
        public int Status { get; set; }

        [Range(1, 10, ErrorMessage = "Рейтинг должен быть от 1 до 10")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "IsBought является обязательным")]
        public bool IsBought { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        public Guid GameId { get; set; }
    }
}
