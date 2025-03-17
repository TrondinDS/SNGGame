using System.ComponentModel.DataAnnotations;

namespace UserActivityService.DB.DTO.UserReaction
{
    public class UserReactionDTO
    {
        // Уникальный идентификатор реакции
        [Range(1, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public int Id { get; set; }

        // Идентификатор пользователя, оставившего реакцию
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserId является обязательным")]
        public Guid UserId { get; set; }

        // Идентификатор комментария, на который дана реакция
        [Range(1, int.MaxValue, ErrorMessage = "CommentId должен быть положительным числом")]
        [Required(ErrorMessage = "CommentId является обязательным")]
        public int CommentId { get; set; }
    }
}
