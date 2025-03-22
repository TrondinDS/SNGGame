using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Organizer
{
    public class UpdateOrganizerDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        
        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(255, ErrorMessage = "Mail cannot be longer than 255 characters")]
        public string Mail { get; set; }
        public int OwnerId { get; set; }
    }
}
