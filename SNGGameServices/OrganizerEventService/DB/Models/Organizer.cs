using System.ComponentModel.DataAnnotations;
using Library;

namespace OrganizerEventService.DB.Models
{
    public class Organizer : IIsDeleted
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(255, ErrorMessage = "Mail cannot be longer than 255 characters")]
        public string Mail { get; set; }

        public bool IsPublicationAllowed { get; set; }

        public int CreatorId { get; set; }

        public int OwnerId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
