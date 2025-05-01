using AdministratumService.DB.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ComplainTicket
{
    public class CreateComplainTicketDTO
    {
        public Guid EntityId { get; set; }
        public int EntityType { get; set; }

        [Required(ErrorMessage = "тип жалобы не был указан")]
        public string ComplainType { get; set; }
    }
}
