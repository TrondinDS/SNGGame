using AdministratumService.DB.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ComplainTicket
{
    public class UpdateComplainTicketDTO
    {
        [Required(ErrorMessage = "id не был отправлен")]
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public int EntityType { get; set; }
        public required string ComplainType { get; set; }
        public required string Status { get; set; }
    }
}
