using GetAwaitService.DB.DTO;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using GetAwaitService.Services.UserService.Interfaces;
using Library.Types;

namespace GetAwaitService.Services.UserAccessRightsService
{
    public class UserAccessRightsApiService : IUserAccessRightsService
    {
        private IUserApiService userApiService;
        private IJobApiService jobApiService;
        private IStudioService studioApiService;
        private IGameService gameApiService;

        public UserAccessRightsApiService(
            IUserApiService userApiService, 
            IJobApiService jobApiService, 
            IStudioService studioApiService,
            IGameService gameApiService
            )
        {
            this.userApiService = userApiService;
            this.jobApiService = jobApiService;
            this.studioApiService = studioApiService;
            this.gameApiService = gameApiService;
        }

        public Task<bool> ChekUserRightsEventAsync(Guid userId, Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChekUserRightsOrganizerAsync(Guid userId, Guid entityId)
        {
            throw new NotImplementedException();
        }

        //GAME
        public async Task<bool> ChekUserRightsModerAndAdminGameAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);
            var gameDTO = await gameApiService.GetByIdAsync(entityId);

            if (gameDTO is null)
                return false;

            return
                userRights.StudioOwnerIds?.Contains(gameDTO.StudioId) == true ||
                userRights.StudioModeratorIds?.Contains(gameDTO.StudioId) == true;
        }

        public async Task<bool> ChekUserRightsAdminGameAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);
            var gameDTO = await gameApiService.GetByIdAsync(entityId);

            if (gameDTO is null)
                return false;

            return
                userRights.StudioOwnerIds?.Contains(gameDTO.StudioId) == true;
        }

        //STUDIO
        public async Task<bool> ChekUserRightsModerAndAdminStudioAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);
            var studioDTO = await studioApiService.GetByIdAsync(entityId);

            if (studioDTO is null)
                return false;

            return
                userRights.StudioOwnerIds?.Contains(studioDTO.Id) == true ||
                userRights.StudioModeratorIds?.Contains(studioDTO.Id) == true;
        }

        public async Task<bool> ChekUserRightsAdminStudioAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);
            var studioDTO = await studioApiService.GetByIdAsync(entityId);

            if (studioDTO is null)
                return false;

            return
                userRights.StudioOwnerIds?.Contains(studioDTO.Id) == true;

        }

        //GLOBAL
        public async Task<bool> ChekUserRightsModerAndAdminGlobalAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);

            return
                userRights.IsAdmin == true ||
                userRights.IsGlobalModerator == true;
        }

        public async Task<bool> ChekUserRightsAdminGlobalAsync(Guid userId, Guid entityId)
        {
            var userRights = await GetUserRightsAsync(userId);

            return
                userRights.IsAdmin == true;
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
                    .Where(j =>  j.EntityType == (int)EntityType.Type.Studio && j.DateFinish > DateTime.UtcNow)
                        .Select(j => j.Id).ToList();

            if (studiosDTO is not null && studiosDTO.Any())
                result.StudioOwnerIds = studiosDTO.Select(s => s.Id).ToList();

            return result;
        }
    }
}
