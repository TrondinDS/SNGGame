using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace GetAwaitService.DB.DTO.UserService.UserSubscription
{
    public class UserSubscriptionDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле EntityId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityId должен быть положительным числом")]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Поле EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public int EntityType { get; set; }

        [Required(ErrorMessage = "Поле DateStart является обязательным")]
        [DataType(DataType.Date, ErrorMessage = "DateStart должен иметь формат даты")]
        public DateTime DateStart { get; set; }

        [DataType(DataType.Date, ErrorMessage = "DateFinish должен иметь формат даты")]
        public DateTime? DateFinish { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "Поле UserId является обязательным")]
        public Guid UserId { get; set; }
    }
}
