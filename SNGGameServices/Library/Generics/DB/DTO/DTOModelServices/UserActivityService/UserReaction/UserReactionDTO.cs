using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction
{
    public class UserReactionDTO
    {
        // Уникальный идентификатор реакции
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public Guid Id { get; set; }

        // Идентификатор пользователя, оставившего реакцию
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserId является обязательным")]
        public Guid UserId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "CommentId должен быть положительным числом")]
        public int ReactionType { get; set; }

        // Идентификатор комментария, на который дана реакция
        [Range(1, int.MaxValue, ErrorMessage = "CommentId должен быть положительным числом")]
        [Required(ErrorMessage = "CommentId является обязательным")]
        public Guid CommentId { get; set; }
    }
}
