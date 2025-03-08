using Library;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministratumService.DB.Models
{
    public class Message : IIsDeleted
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        // Навигационные свойства
        public int ChatFeedbackId { get; set; }
        [ForeignKey("ChatFeedbackId")]
        public ChatFeedback ChatFeedback { get; set; }
    }
}
