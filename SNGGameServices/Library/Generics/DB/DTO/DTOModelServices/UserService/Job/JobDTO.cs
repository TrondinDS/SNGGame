using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.Job
{
    public class JobDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "EntityId является обязательным")]
        public Guid EntityId { get; set; }

        [Required(ErrorMessage = "EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public required int EntityType { get; set; }

        [Required(ErrorMessage = "IsModerator является обязательным")]
        public bool IsModerator { get; set; }

        [Required(ErrorMessage = "DateStart является обязательным")]
        [DataType(DataType.Date, ErrorMessage = "DateStart должно иметь формат даты")]
        public DateTime DateStart { get; set; }

        [DataType(DataType.Date, ErrorMessage = "DateFinish должно иметь формат даты")]
        public DateTime? DateFinish { get; set; }

        [MaxLength(255, ErrorMessage = "Position не может превышать 255 символов")]
        [MinLength(1, ErrorMessage = "Position должна содержать хотя бы один символ")]
        public string Position { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.Date, ErrorMessage = "DateDeleted должно иметь формат даты")]
        public DateTime? DateDeleted { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserId является обязательным")]
        public Guid UserId { get; set; }
    }
}
