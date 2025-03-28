using System.ComponentModel.DataAnnotations;

namespace GetAwaitService.DB.DTO.UserService.User
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Поле Name является обязательным")]
        [MaxLength(255, ErrorMessage = "Поле Name должно иметь длинну до 255 символов")]
        [MinLength(2, ErrorMessage = "Поле Name должно иметь длинну от 2 символов")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Поле DateBirth является обязательным")]
        [DataType(DataType.Date)]
        [MinimumAge(18)]
        public DateTime? DateBirth { get; set; }

        [EmailAddress(ErrorMessage = "Поле Email имеет неверный формат")]
        [MaxLength(255, ErrorMessage = "Email должно иметь длинну до 255 символов")]
        public string? Email { get; set; }

        [Display(Name = "Является администратором")]
        public bool? IsAdmin { get; set; }

        [Display(Name = "Является глобальным модератором")]
        public bool? IsGlobalModerator { get; set; }
    }
}
