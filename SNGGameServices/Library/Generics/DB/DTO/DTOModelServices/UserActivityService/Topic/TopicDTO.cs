using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic
{
    public class TopicDTO
    {
        // Уникальный идентификатор сущности
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public Guid Id { get; set; }

        // Идентификатор связанной сущности
        [Required(ErrorMessage = "EntityId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityId должен быть положительным числом")]
        public Guid EntityId { get; set; }

        // Тип связанной сущности
        [Required(ErrorMessage = "EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public int EntityType { get; set; }

        // Заголовок сущности
        [Required(ErrorMessage = "Title является обязательным")]
        [MaxLength(255, ErrorMessage = "Title не может превышать 255 символов")]
        [MinLength(1, ErrorMessage = "Title должна содержать хотя бы один символ")]
        public required string Title { get; set; }

        // Описание сущности (опционально)
        [MaxLength(1000, ErrorMessage = "Description не может превышать 1000 символов")]
        public string Description { get; set; }

        // Дата создания сущности
        [Required(ErrorMessage = "DateCreated является обязательным")]
        [DataType(DataType.DateTime, ErrorMessage = "DateCreated должно иметь формат даты")]
        public DateTime DateCreated { get; set; }

        // Идентификатор пользователя-создателя
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserCreatorId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserCreatorId является обязательным")]
        public Guid UserCreatorId { get; set; }

        // Признак удаления сущности
        public bool IsDeleted { get; set; } = false;

        // Дата удаления сущности (если есть)
        [DataType(DataType.DateTime, ErrorMessage = "DateDeleted должно иметь формат даты")]
        public DateTime? DateDeleted { get; set; }
    }
}
