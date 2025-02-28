using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministratumService.DB.Models
{
    public class UserComplains 
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public ComplainTicket Ticket { get; set; }
    }
}
