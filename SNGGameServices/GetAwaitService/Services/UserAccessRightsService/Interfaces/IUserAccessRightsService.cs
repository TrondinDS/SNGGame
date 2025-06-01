using GetAwaitService.DB.DTO;

namespace GetAwaitService.Services.UserAccessRightsService.Interfaces
{
    public interface IUserAccessRightsService
    {
        Task<UserAccessRightsDTO> GetUserRightsAsync(Guid userId);


        Task<bool> ChekUserRightsAdminGlobalAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsModerAndAdminGlobalAsync(Guid userId, Guid entityId);


        Task<bool> ChekUserRightsAdminStudioAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsModerAndAdminStudioAsync(Guid userId, Guid entityId);

        Task<bool> ChekUserRightsAdminGameAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsModerAndAdminGameAsync(Guid userId, Guid entityId);


        Task<bool> ChekUserRightsOrganizerAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsEventAsync(Guid userId, Guid entityId);
    }
}
