using AdministratumService.DB.Models;

namespace AdministratumService.DB.DTO.ChatFeedback
{
    public class GetByIdChatFeedbackDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
