using Library;

namespace AdministratumService.DB.Models
{
    public class ComplainTicket : IIsDeleted, IEntity
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
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
