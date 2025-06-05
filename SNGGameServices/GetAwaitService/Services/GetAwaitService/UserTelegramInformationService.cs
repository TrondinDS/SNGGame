using GetAwaitService.DB.Models;
using GetAwaitService.Repository;
using GetAwaitService.Services.GetAwaitService.Interfaces;

namespace GetAwaitService.Services.GetAwaitService
{
    public class UserTelegramInformationService : IUserTelegramInformationService
    {
        protected readonly IUserTelegramInformationRepository userTelegramInformationRepository;

        public UserTelegramInformationService(IUserTelegramInformationRepository userTelegramInformationRepository)
        {
            this.userTelegramInformationRepository = userTelegramInformationRepository;
        }

        public async Task AddAsync(UserTelegramInformation userInfo)
        {
            await userTelegramInformationRepository.AddAsync(userInfo);
            await userTelegramInformationRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await userTelegramInformationRepository.GetByIdAsync(id);
            if (user != null)
            {
                await userTelegramInformationRepository.DeleteAsync(user);
                await userTelegramInformationRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<UserTelegramInformation>> GetAllAsync()
        {
            return userTelegramInformationRepository.GetAllAsync();
        }

        public async Task<UserTelegramInformation> GetByIdAsync(Guid id)
        {
            return await userTelegramInformationRepository.GetByIdAsync(id);
        }

        public async Task<UserTelegramInformation> GetUserTgInfoFromTgId(ulong tgId)
        {
            return await userTelegramInformationRepository.GetUserTgInfoFromTgId(tgId);
        }

        public async Task UpdateAsync(UserTelegramInformation userInfo)
        {
            await userTelegramInformationRepository.UpdateAsync(userInfo);
            await userTelegramInformationRepository.SaveChangesAsync();
        }
    }
}
