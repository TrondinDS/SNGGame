using Library;

namespace AdministratumService.DB.Models
{
    public class ComplainTicket : IIsDeleted
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public string ComplainType { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime StatusUpdateDatetime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }

        public ICollection<UserComplains> UserComplains { get; set; }
    }
}
