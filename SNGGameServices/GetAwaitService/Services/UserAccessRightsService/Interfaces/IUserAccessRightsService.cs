using GetAwaitService.DB.DTO;

namespace GetAwaitService.Services.UserAccessRightsService.Interfaces
{
    public interface IUserAccessRightsService
    {
        Task<UserAccessRightsDTO> GetUserRightsAsync(Guid userId);
        Task<bool> ChekUserRightsStudioAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsGameAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsOrganizerAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsEventAsync(Guid userId, Guid entityId);
    }
}
