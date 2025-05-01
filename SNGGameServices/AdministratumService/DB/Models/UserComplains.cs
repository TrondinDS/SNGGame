using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library;

namespace AdministratumService.DB.Models
{
    public class UserComplains
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid TicketId { get; set; }

        [ForeignKey("TicketId")]
        public ComplainTicket Ticket { get; set; }
    }
}
