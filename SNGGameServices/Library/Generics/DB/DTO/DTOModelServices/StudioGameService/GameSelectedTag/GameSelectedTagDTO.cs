using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag
{
    public class GameSelectedTagDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "TagId является обязательным")]
        public Guid TagId { get; set; }
    }
}
