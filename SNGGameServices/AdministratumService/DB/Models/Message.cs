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
        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTime? DateDeleted
        {
            get => EnsureUtc(_dateDeleted);
            set => _dateDeleted = ConvertToUtc(value);
        }

        [NotMapped] // Указывает EF Core игнорировать это поле
        private DateTime? _dateDeleted;

        public Guid ChatFeedbackId { get; set; }

        [ForeignKey("ChatFeedbackId")]
        public ChatFeedback ChatFeedback { get; set; }

        // Метод для обеспечения UTC при чтении
        private static DateTime? EnsureUtc(DateTime? value)
        {
            return value == null ? null : DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
        }

        private static DateTime? ConvertToUtc(DateTime? value)
        {
            return value?.ToUniversalTime();
        }
    }
}

