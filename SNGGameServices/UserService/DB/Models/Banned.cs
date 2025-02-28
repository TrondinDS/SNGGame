using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.DB.Models;

namespace BannedService.DB.Models
{
    public class Banned : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public required string EntityType { get; set; }
        public required string Reason { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public int TypePunishment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }

        // Внешний ключ для модератора
        public int UserIdModerator { get; set; }
        [ForeignKey("UserIdModerator")]
        public User UserModerator { get; set; } // Навигационное свойство для модератора

        // Внешний ключ для забаненного пользователя
        public int UserIdBanned { get; set; }
        [ForeignKey("UserIdBanned")]
        public User UserBanned { get; set; } // Навигационное свойство для забаненного пользователя
    }
}
