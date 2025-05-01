using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag
{
    public class GameSelectedTagDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "GameId должен быть положительным числом")]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "TagId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "TagId должен быть положительным числом")]
        public Guid TagId { get; set; }
    }
}
