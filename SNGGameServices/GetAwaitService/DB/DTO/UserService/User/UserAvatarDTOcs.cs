using System.ComponentModel.DataAnnotations;
using Library.Attributes;

namespace GetAwaitService.DB.DTO.UserService.User
{
    public class UserAvatarDTO
    {
        [Required(ErrorMessage = "UserId is required.")]
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserId должен быть корректным GUID"
        )]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Image file is required.")]
        [FileNotEmpty]
        public IFormFile ImageFile { get; set; }
    }
}
