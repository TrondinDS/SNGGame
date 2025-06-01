using GetAwaitService.DB.DTO;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;

namespace GetAwaitService.Services.UserAccessRightsService.Interfaces
{
    public interface IUserAccessRightsService
    {
        Task<UserAccessRightsDTO> GetUserRightsAsync(Guid userId);


        Task<bool> ChekUserRightsAdminGlobalAsync(Guid userId);
        Task<bool> ChekUserRightsModerAndAdminGlobalAsync(Guid userId);


        Task<bool> ChekUserRightsAdminStudioAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsModerAndAdminStudioAsync(Guid userId, Guid entityId);

        Task<bool> ChekUserRightsAdminGameAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsModerAndAdminGameAsync(Guid userId, Guid entityId);

        Task<bool> ChekUserRightsModerAndAdminBanGlobalAndLocalAsync(Guid userId, BannedCreateDTO entity);

        Task<bool> ChekUserRightsBanned(Guid userId, TopicCreateDTO entity);
        Task<bool> ChekUserRightsBanned(Guid userId, CommentCreateDTO entity);


        Task<bool> ChekUserRightsOrganizerAsync(Guid userId, Guid entityId);
        Task<bool> ChekUserRightsEventAsync(Guid userId, Guid entityId);
    }
}
