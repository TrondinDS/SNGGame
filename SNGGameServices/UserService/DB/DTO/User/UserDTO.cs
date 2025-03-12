using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace UserService.DB.DTO.User
{
    public class UserDTO
    {
        [RegularExpression("^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$", ErrorMessage = "UserId должен быть корректным GUID")]
        public Guid Id { get; set; }

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

        [Range(1, int.MaxValue, ErrorMessage = "KeyIdPhotoAvatar должен быть положительным числом")]
        public int? KeyIdPhotoAvatar { get; set; }
        [Display(Name = "Является администратором")]
        public bool? IsAdmin { get; set; }
        [Display(Name = "Является глобальным модератором")]
        public bool? IsGlobalModerator { get; set; }
    }
}
