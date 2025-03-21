using System.ComponentModel.DataAnnotations.Schema;

namespace AdministratumService.DB.DTO.Message
{
    public class GetByIdMessageDTO
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
