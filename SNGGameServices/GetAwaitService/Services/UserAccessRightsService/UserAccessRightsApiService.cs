using AutoMapper;
using GetAwaitService.DB.DTO;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using GetAwaitService.Services.UserActivityService.Interfaces;
using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Library.Types;

namespace GetAwaitService.Services.UserAccessRightsService
{
    public class UserAccessRightsApiService : IUserAccessRightsService
    {
        private IUserApiService userApiService;
        private IJobApiService jobApiService;
        private IStudioService studioApiService;
        private IGameService gameApiService;
        private IBannedApiService bannedApiService;
        private ITopicApiService topicApiService;
        private IMapper mapper;

        public UserAccessRightsApiService(
            IUserApiService userApiService, 
            IJobApiService jobApiService, 
            IStudioService studioApiService,
            IGameService gameApiService,
            IBannedApiService bannedApiService,
            ITopicApiService topicApiService,
            IMapper mapper

            )
        {
            this.userApiService = userApiService;
            this.jobApiService = jobApiService;
            this.studioApiService = studioApiService;
            this.gameApiService = gameApiService;
            this.bannedApiService = bannedApiService;
            this.topicApiService = topicApiService;
            this.mapper = mapper;
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


        //BANNED
        public async Task<bool> ChekUserRightsModerAndAdminBanGlobalAndLocalAsync(Guid userId, BannedCreateDTO entity)
        {
            if (entity.TypePunishment < 11)
            {
                return await ChekUserRightsModerAndAdminGlobalAsync(userId);
            }

            if (entity.TypePunishment > 100)
            {
                switch (entity.EntityType)
                {
                    case (int)EntityType.Type.Studio:
                        return await ChekUserRightsModerAndAdminStudioAsync(userId, entity.EntityId);
                        break;

                    case (int)EntityType.Type.Game:
                        return await ChekUserRightsModerAndAdminGameAsync(userId, entity.EntityId);
                        break;

                    case (int)EntityType.Type.Event:
                        throw new Exception("Отсутсвие реализации");
                        break;

                    case (int)EntityType.Type.Organizer:
                        throw new Exception("Отсутсвие реализации");
                        break;

                    default:
                        throw new NotSupportedException($"Entity type {entity.EntityType} is not supported for banning.");
                }
            }
            return false;
        }


        //TOPIC COMMENT
        public async Task<bool> ChekUserRightsBanned(Guid userId, TopicCreateDTO entity)
        {
            var userRightsBans = await bannedApiService.GetBannedsByUserId(userId);

            switch (entity.EntityType)
            {
                case (int)EntityType.Type.Studio:
                    {
                        bool hasActiveBan = userRightsBans.Any(b =>
                            b.EntityId == entity.EntityId &&
                            b.TypePunishment == (int)PunishmentType.Type.BanWriting &&
                            b.DateFinish > DateTime.UtcNow);

                        return !hasActiveBan;
                    }

                case (int)EntityType.Type.Game:
                    {
                        var game = await gameApiService.GetByIdAsync(entity.EntityId);
                        if (game == null)
                            return false;

                        bool hasActiveBan = userRightsBans.Any(b =>
                            (b.EntityId == game.StudioId || b.EntityId == game.Id) &&
                            b.TypePunishment == (int)PunishmentType.Type.BanWriting &&
                            b.DateFinish > DateTime.UtcNow);

                        return !hasActiveBan;
                    }

                case (int)EntityType.Type.Event:
                    throw new Exception("Отсутсвие реализации");
                    break;

                case (int)EntityType.Type.Organizer:
                    throw new Exception("Отсутсвие реализации");
                    break;

                default:
                    throw new NotSupportedException($"Entity type {entity.EntityType} is not supported for banning.");
            }

            return false;

        }

        public async Task<bool> ChekUserRightsBanned(Guid userId, CommentCreateDTO entity)
        {
            var topic = await topicApiService.GetByIdAsync(entity.TopicId);
            if (topic is null)
                return false;   
            var topicC = mapper.Map<TopicCreateDTO>(topic);
            return await ChekUserRightsBanned(userId, topicC);
        }


        //GLOBAL
        public async Task<bool> ChekUserRightsModerAndAdminGlobalAsync(Guid userId)
        {
            var userRights = await GetUserRightsAsync(userId);

            return
                userRights.IsAdmin == true || 
                    userRights.IsGlobalModerator == true;
        }

        public async Task<bool> ChekUserRightsAdminGlobalAsync(Guid userId)
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
                    .Where(j =>  
                        j.EntityType == (int)EntityType.Type.Studio && 
                        (j.DateFinish > DateTime.UtcNow || j.DateFinish is null) &&
                        j.IsModerator == true
                    ).Select(j => j.EntityId).ToList();

            if (studiosDTO is not null && studiosDTO.Any())
                result.StudioOwnerIds = studiosDTO.Where(s => s.OwnerId == userId)
                    .Select(s => s.Id).ToList();

            return result;
        }
    }
}
