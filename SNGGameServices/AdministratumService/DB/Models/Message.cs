using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library;

namespace AdministratumService.DB.Models
{
    public class Message : IIsDeleted
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public Guid ChatFeedbackId { get; set; }

        [ForeignKey("ChatFeedbackId")]
        public ChatFeedback ChatFeedback { get; set; }
    }
}
