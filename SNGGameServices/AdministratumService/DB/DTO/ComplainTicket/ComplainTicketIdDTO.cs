using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ComplainTicket
{
    public class ComplainTicketIdDTO
    {
        [Required(ErrorMessage = "id не был отправлен")]
        public int Id { get; set; }
    }
}
