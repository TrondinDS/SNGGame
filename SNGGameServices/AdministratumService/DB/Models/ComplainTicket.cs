using Library;
using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.Models
{
    public class ComplainTicket : IIsDeleted, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public int EntityType { get; set; }
        public required string ComplainType { get; set; }
        public required string Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime StatusUpdateDatetime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public ICollection<UserComplains> UserComplains { get; set; }
    }
}
