using System.ComponentModel.DataAnnotations.Schema;

namespace AdministratumService.DB.DTO.Message
{
    public class CreateMessageDTO
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public Guid ChatFeedbackId { get; set; }
    }
}
