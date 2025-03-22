using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Organizer
{
    public class GetByIdOrganizerDTO
    {
        public string Title { get; set; }
        public string Mail { get; set; }
        public bool IsPublicationAllowed { get; set; }
        public int CreatorId { get; set; }
        public int OwnerId { get; set; }
    }
}
