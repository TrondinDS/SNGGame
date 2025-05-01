using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.Banned
{
    public class BannedDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "EntityId является обязательным")]
        public Guid EntityId { get; set; }

        [Required(ErrorMessage = "EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public required int EntityType { get; set; }

        [Required(ErrorMessage = "Reason является обязательным")]
        [MaxLength(255, ErrorMessage = "Reason не может превышать 255 символов")]
        [MinLength(1, ErrorMessage = "Reason должна содержать хотя бы один символ")]
        public required string Reason { get; set; }

        [Required(ErrorMessage = "DateStart является обязательным")]
        [DataType(DataType.Date, ErrorMessage = "DateStart должно иметь формат даты")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "DateFinish является обязательным")]
        [DataType(DataType.Date, ErrorMessage = "DateFinish должно иметь формат даты")]
        public DateTime DateFinish { get; set; }

        [Required(ErrorMessage = "TypePunishment является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "TypePunishment должен быть положительным числом")]
        public int TypePunishment { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.Date, ErrorMessage = "DateDeleted должно иметь формат даты")]
        public DateTime? DateDeleted { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserIdModerator является обязательным")]
        public Guid UserIdModerator { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserIdBanned является обязательным")]
        public Guid UserIdBanned { get; set; }
    }
}
