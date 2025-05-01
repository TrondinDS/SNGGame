using AdministratumService.DB.Models;

namespace AdministratumService.DB.DTO.ComplainTicket
{
    public class GetByIdComplainTicketDTO
    {
        public Guid EntityId { get; set; }
        public int EntityType { get; set; }
        public required string ComplainType { get; set; }
        public required string Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime StatusUpdateDatetime { get; set; }
    }
}
