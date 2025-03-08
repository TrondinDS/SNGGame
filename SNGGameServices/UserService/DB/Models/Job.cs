using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using UserService.DB.Models;

namespace StudioGameService.DB.Model
{
    public class Job : IIsDeleted, IEntity
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public required int EntityType { get; set; }
        public bool IsModerator { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; } 
        [MaxLength(255, ErrorMessage = "Position cannot exceed 255 characters")]
        public string Position { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } // Навигационное свойство для модератора
    }
}
