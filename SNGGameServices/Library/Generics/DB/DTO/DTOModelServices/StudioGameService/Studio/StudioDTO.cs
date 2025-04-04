using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio
{
    public class StudioDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название является обязательным")]
        [MaxLength(255, ErrorMessage = "Название не должно превышать 255 символов")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Email является обязательным")]
        [MaxLength(255, ErrorMessage = "Email не должен превышать 255 символов")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Флаг разрешения публикации является обязательным")]
        public bool IsResolutionPublication { get; set; }

        [Required(ErrorMessage = "Идентификатор создателя является обязательным")]
        public Guid CreatorId { get; set; }

        [Required(ErrorMessage = "Идентификатор владельца является обязательным")]
        public Guid OwnerId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
