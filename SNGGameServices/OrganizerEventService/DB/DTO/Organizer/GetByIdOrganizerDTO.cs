using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Organizer
{
    public class GetByIdOrganizerDTO
    {
        public string Title { get; set; }
        public string Mail { get; set; }
        public bool IsPublicationAllowed { get; set; }
        public Guid CreatorId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
