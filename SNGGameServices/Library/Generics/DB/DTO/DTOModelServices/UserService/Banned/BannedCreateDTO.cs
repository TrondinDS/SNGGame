using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.Banned
{
    public class BannedCreateDTO
    {
        [Required(ErrorMessage = "EntityId является обязательным")]
        public Guid EntityId { get; set; }

        [Required(ErrorMessage = "EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public required int EntityType { get; set; }

        [Required(ErrorMessage = "Reason является обязательным")]
        [MaxLength(255, ErrorMessage = "Reason не может превышать 255 символов")]
        [MinLength(1, ErrorMessage = "Reason должна содержать хотя бы один символ")]
        public required string Reason { get; set; }

        [Required(ErrorMessage = "DateFinish является обязательным")]
        [DataType(DataType.Date, ErrorMessage = "DateFinish должно иметь формат даты")]
        public DateTime DateFinish { get; set; }

        [Required(ErrorMessage = "TypePunishment является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "TypePunishment должен быть положительным числом")]
        public int TypePunishment { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserIdBanned должен быть корректным GUID"
        )]
        [Required(ErrorMessage = "UserIdBanned является обязательным")]
        public Guid UserIdBanned { get; set; }
    }
}
