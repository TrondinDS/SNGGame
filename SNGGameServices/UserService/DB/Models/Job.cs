using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.DB.Models;

namespace StudioGameService.DB.Model
{
    public class Job : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public required string EntityType { get; set; }
        [Required]
        public bool IsModerator { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; } 
        [MaxLength(255)]
        public string Position { get; set; }
        public bool IsDeleted { get; set    ; }
        public DateTime DateDeleted { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } // Навигационное свойство для модератора
    }
}
