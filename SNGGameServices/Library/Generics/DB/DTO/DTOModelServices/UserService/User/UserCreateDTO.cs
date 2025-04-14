using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.User
{
    public class UserCreateDTO
    {
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
