using GetAwaitService.DB.DTO;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;
using Library.Types;

namespace GetAwaitService.Services.UserAccessRightsService
{
    public class UserAccessRightsApiService : IUserAccessRightsService
    {
        private IUserApiService userApiService;
        private IJobApiService jobApiService;
        private IStudioService studioApiService;
        private IGameService gameService;

        public UserAccessRightsApiService(
            IUserApiService userApiService, 
            IJobApiService jobApiService, 
            IStudioService studioApiService,
            IGameService gameService
            )
        {
            this.userApiService = userApiService;
            this.jobApiService = jobApiService;
            this.studioApiService = studioApiService;
            this.gameService = gameService;
        }

        public Task<bool> ChekUserRightsEventAsync(Guid userId, Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChekUserRightsOrganizerAsync(Guid userId, Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChekUserRightsGameAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);
            var gameDTO = await gameService.GetByIdAsync(entityId);

            if (gameDTO is null)
                return false;

            return
                userRights.StudioOwnerIds?.Contains(gameDTO.StudioId) == true ||
                userRights.StudioModeratorIds?.Contains(gameDTO.StudioId) == true;
        }

        public Task<bool> ChekUserRightsStudioAsync(Guid userId, Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAccessRightsDTO> GetUserRightsAsync(Guid userId)
        {
            var userDTO = await userApiService.GetUserByIdAsync(userId);
            var jobsDTO = await jobApiService.GetJobsByUserIdAsync(userId);
            var studiosDTO = await studioApiService.GetStudioByUserIdAsync(userId);

            if (userDTO is null)
                throw new Exception($"Пользователя с id = {userId} не существует.");

            var result = new UserAccessRightsDTO
            {
                UserId = userDTO.Id,
                IsAdmin = userDTO.IsAdmin,
                IsGlobalModerator = userDTO.IsGlobalModerator,
            };

            if (jobsDTO is not null && jobsDTO.Any())
                result.StudioModeratorIds = jobsDTO
                    .Where(j => j.EntityType == (int)EntityType.Type.Studio)
                    .Select(j => j.Id).ToList();

            if (studiosDTO is not null && studiosDTO.Any())
                result.StudioOwnerIds = studiosDTO.Select(s => s.Id).ToList();

            return result;
        }
    }
}
