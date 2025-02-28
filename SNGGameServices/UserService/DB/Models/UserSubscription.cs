using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserService.DB.Models
{
    public class UserSubscription
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public required string EntityType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
