using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ChatFeedback
{
    public class UpdateChatFeedbackDTO
    {
        [Required(ErrorMessage = "id чата не был отправлен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не было указано новое название заголовка чата")]
        [StringLength(100, ErrorMessage = "Название заголовка чата не должно превышать 100 символов")]
        public string Title { get; set; }
    }
}
