using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer
{
    public class OrganizerDTO
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
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        [Required(ErrorMessage = "Image является обязательным")]
        public string Image { get; set; }

        [Required(ErrorMessage = "ImageType является обязательным")]
        public string ImageType { get; set; }

        [Required(ErrorMessage = "Content является обязательным")]
        public string Content { get; set; }
    }
}
