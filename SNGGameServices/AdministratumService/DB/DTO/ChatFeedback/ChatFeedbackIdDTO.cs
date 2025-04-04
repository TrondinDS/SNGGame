using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ChatFeedback
{
    public class ChatFeedbackIdDTO
    {
        [Required(ErrorMessage = "id чата не был отправлен")]
        public Guid Id { get; set; }
    }
}
