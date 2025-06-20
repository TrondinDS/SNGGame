namespace GetAwaitService.DB.DTO
{
    public class UserAccessRightsDTO
    {
        public Guid UserId { get; set; }

        public bool? IsAdmin { get; set; }
        public bool? IsGlobalModerator { get; set; }

        public List<Guid> StudioModeratorIds { get; set; } = new();
        public List<Guid> StudioOwnerIds { get; set; } = new();

        public List<Guid> OrganizerModeratorIds { get; set; } = new();
        public List<Guid> OrganizerOwnderIds { get; set; } = new();
    }
}
