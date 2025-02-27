using Library;
using System.ComponentModel.DataAnnotations;

namespace UserService.DB.Models
{
    public class User : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
        public required string Email { get; set; }
        public required string FilepathToPhotoIcon { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsGlobalModerator { get; set; } = false;
        public bool IsDelet { get; set; } = false;
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
