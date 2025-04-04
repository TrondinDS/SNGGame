using AdministratumService.DB.Models;

namespace AdministratumService.DB.DTO.ChatFeedback
{
    public class GetByIdChatFeedbackDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

    }
}
