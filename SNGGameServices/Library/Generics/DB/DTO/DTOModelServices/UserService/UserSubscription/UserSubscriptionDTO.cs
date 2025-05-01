using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription
{
    public class UserSubscriptionDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле EntityId является обязательным")]
        public Guid EntityId { get; set; }

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
