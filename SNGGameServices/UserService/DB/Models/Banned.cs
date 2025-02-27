using Library;
using System.ComponentModel.DataAnnotations;

namespace BannedService.DB.Models
{
    public class Banned : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        public int UserIdModerator { get; set; }
        public int UserIdBanned { get; set; }
        public int EntityId { get; set; }
        public required string EntityType { get; set; }
        public required string Reason { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public int TypePunishment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
