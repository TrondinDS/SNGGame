using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment
{
    public class CommentDTO
    {
        // Уникальный идентификатор комментария
        public Guid Id { get; set; }

        // Идентификатор пользователя, оставившего комментарий
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserId является обязательным")]
        public Guid UserId { get; set; }

        // Текст комментария
        [Required(ErrorMessage = "Body является обязательным")]
        [MaxLength(1000, ErrorMessage = "Body не может превышать 1000 символов")]
        [MinLength(1, ErrorMessage = "Body должна содержать хотя бы один символ")]
        public required string Body { get; set; }

        // Дата создания комментария
        [Required(ErrorMessage = "DateCreated является обязательным")]
        [DataType(DataType.DateTime, ErrorMessage = "DateCreated должно иметь формат даты")]
        public DateTime DateCreated { get; set; }

        // Признак удаления комментария
        public bool IsDeleted { get; set; } = false;

        // Дата удаления комментария (если есть)
        [DataType(DataType.DateTime, ErrorMessage = "DateDeleted должно иметь формат даты")]
        public DateTime? DateDeleted { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "NumberOrder должено быть положительным числом")]
        public int CountLike { get; set; }

        // Идентификатор темы, к которой относится комментарий
        [Required(ErrorMessage = "TopicId является обязательным")]
        public Guid TopicId { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "CommentIdReference должен быть корректным GUID"
        )]
        public Guid? CommentIdReference { get; set; }
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "CommentIdResponse должен быть корректным GUID"
        )]
        public Guid? CommentIdResponse { get; set; }
    }
}
