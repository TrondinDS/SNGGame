using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Library;

namespace UserService.DB.Models
{
    public class UserSubscription : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int EntityType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
