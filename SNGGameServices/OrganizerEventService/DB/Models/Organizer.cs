using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library;

namespace OrganizerEventService.DB.Models
{
    public class Organizer : IIsDeleted
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(255, ErrorMessage = "Mail cannot be longer than 255 characters")]
        public string Mail { get; set; }

        public bool IsPublicationAllowed { get; set; }

        public Guid CreatorId { get; set; }

        public Guid OwnerId { get; set; }
        public bool IsDeleted { get; set; } = false;
        [DataType(DataType.DateTime)]
        public DateTime? DateDeleted
        {
            get => EnsureUtc(_dateDeleted);
            set => _dateDeleted = ConvertToUtc(value);
        }

        [NotMapped] // Указывает EF Core игнорировать это поле
        private DateTime? _dateDeleted;

        public ICollection<Event> Events { get; set; } = new List<Event>();

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
