using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Organizer
{
    public class CreateOrganizerDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(255, ErrorMessage = "Mail cannot be longer than 255 characters")]
        public string Mail { get; set; }

        public Guid CreatorId { get; set; }
        public Guid OwnerId { get; set; }

    }
}
