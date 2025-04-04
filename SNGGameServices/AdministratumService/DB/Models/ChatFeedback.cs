using System.ComponentModel.DataAnnotations;

namespace AdministratumService.DB.Models
{
    public class ChatFeedback
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
