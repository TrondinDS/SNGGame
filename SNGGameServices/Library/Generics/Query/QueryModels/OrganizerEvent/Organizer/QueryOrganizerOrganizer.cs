    using System;
    namespace Library.Generics.Query.QueryModels.OrganizerEvent.Organizer;

    public class QueryOrganizerOrganizer
    {
        public List<Guid>? OrganizerId { get; set; }
        public string? Title { get; set; }
        public string? Mail { get; set; }
        public List<Guid>? CreatorId { get; set; }
        public List<Guid>? OwnerId { get; set; }
}
