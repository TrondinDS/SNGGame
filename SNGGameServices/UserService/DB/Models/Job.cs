using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Library;
using UserService.DB.Models;

namespace StudioGameService.DB.Model
{
    public class Job : IIsDeleted, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public required int EntityType { get; set; }
        public bool IsModerator { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; }

        [MaxLength(255, ErrorMessage = "Position cannot exceed 255 characters")]
        public string Position { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } // Навигационное свойство для модератора
    }
}
