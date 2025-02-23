using System.ComponentModel.DataAnnotations;
using UserService.DB.Models.Interfaces;

namespace UserService.DB.Models
{
    public class User : IsDeleted
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateOnly DateBirth { get; set; }
        public required string Email { get; set; }
        public required string FilepathToDescription { get; set; }
        public required string FilepathToPhotoIcon { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsGlobalModerator { get; set; } = false;
        public bool IsDelet { get; set; } = false;
    }
}
