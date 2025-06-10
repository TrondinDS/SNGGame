using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library;

namespace OrganizerEventService.DB.Models
{
    public class Event : IIsDeleted
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(255, ErrorMessage = "Address cannot be longer than 255 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100, ErrorMessage = "Country cannot be longer than 100 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Region is required")]
        [MaxLength(100, ErrorMessage = "Region cannot be longer than 100 characters")]
        public string Region { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City cannot be longer than 100 characters")]
        public string City { get; set; }

        // Геометрическая метка в виде строки URL
        [Url(ErrorMessage = "Invalid URL format for GeoUrl")]
        [MaxLength(1024, ErrorMessage = "GeoUrl cannot be longer than 1024 characters")]
        public string GeoUrl { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }


        [Required(ErrorMessage = "OrganizerEventId is required")]
        public Guid OrganizerEventId { get; set; }

        [ForeignKey("OrganizerEventId")]
        public virtual Organizer Organizer { get; set; }
        public bool IsDeleted { get; set; } = false;
        [DataType(DataType.DateTime)]
        public DateTime? DateDeleted
        {
            get => EnsureUtc(_dateDeleted);
            set => _dateDeleted = ConvertToUtc(value);
        }

        [NotMapped] // Указывает EF Core игнорировать это поле
        private DateTime? _dateDeleted;

        //public ICollection<Event> Events { get; set; } = new List<Event>();

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
