using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ComplainTicket
{
    public class DeleteComplainTicketDTO
    {
        [Required(ErrorMessage = "id не был отправлен")]
        public Guid Id { get; set; }
    }
}
