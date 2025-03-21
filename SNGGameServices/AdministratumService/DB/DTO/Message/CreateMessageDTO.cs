using System.ComponentModel.DataAnnotations.Schema;

namespace AdministratumService.DB.DTO.Message
{
    public class CreateMessageDTO
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public int ChatFeedbackId { get; set; }
    }
}
