using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.DTO.ChatFeedback
{
    public class CreateChatFeedbackDTO
    {
        [Required(ErrorMessage = "Не было указано название заголовка чата")]
        [StringLength(100, ErrorMessage = "Название заголовка чата не должно превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Не был прикреплён пользователь к чату")]
        public Guid UserId { get; set; }
    }
}
