using BannedService.DB.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BannedService.DB.Models
{
    public class Banned : IsDeleted
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
        public bool IsDelet { get; set; }
    }
}
