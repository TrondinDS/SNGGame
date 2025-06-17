using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministratumService.DB.Models
{
    public class ComplainTicket : IIsDeleted, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public int EntityType { get; set; }
        public required string ComplainType { get; set; }
        public required string Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime StatusUpdateDatetime { get; set; }
        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTime? DateDeleted
        {
            get => EnsureUtc(_dateDeleted);
            set => _dateDeleted = ConvertToUtc(value);
        }

        [NotMapped] // Указывает EF Core игнорировать это поле
        private DateTime? _dateDeleted;

        public ICollection<UserComplains> UserComplains { get; set; }

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
