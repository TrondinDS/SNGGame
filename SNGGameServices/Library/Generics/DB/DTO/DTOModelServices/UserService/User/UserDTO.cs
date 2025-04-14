using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.User
{
    public class UserDTO
    {
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле Name является обязательным")]
        [MaxLength(255, ErrorMessage = "Поле Name должно иметь длинну до 255 символов")]
        [MinLength(2, ErrorMessage = "Поле Name должно иметь длинну от 2 символов")]
        public required string Name { get; set; }

        [DataType(DataType.Date)]
        [MinimumAge(18)]
        public DateTime? DateBirth { get; set; }

        [Required(ErrorMessage = "Поле Login является обязательным")]
        [MaxLength(255, ErrorMessage = "Login должно иметь длинну до 255 символов")]
        public required string Login { get; set; }

        [Display(Name = "Является администратором")]
        public bool? IsAdmin { get; set; }

        [Display(Name = "Является глобальным модератором")]
        public bool? IsGlobalModerator { get; set; }
    }
}
