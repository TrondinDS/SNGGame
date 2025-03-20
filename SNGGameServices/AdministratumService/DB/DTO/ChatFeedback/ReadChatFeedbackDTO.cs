using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ChatFeedback
{
    public class ReadChatFeedbackDTO
    {
        [Required(ErrorMessage = "id чата не был отправлен")]
        public int Id { get; set; }
    }
}
