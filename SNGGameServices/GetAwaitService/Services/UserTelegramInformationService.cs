using GetAwaitService.DB.Models;
using GetAwaitService.Repository;
using GetAwaitService.Services.Interfaces;

namespace GetAwaitService.Services
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

        public async Task DeleteAsync(int id)
        {
            var user = await userTelegramInformationRepository.GetByIdAsync(id);
            if (user != null)
            {
                userTelegramInformationRepository.DeleteAsync(user);
                await userTelegramInformationRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<UserTelegramInformation>> GetAllAsync()
        {
            return userTelegramInformationRepository.GetAllAsync();
        }

        public async Task<UserTelegramInformation> GetByIdAsync(int id)
        {
            return await userTelegramInformationRepository.GetByIdAsync(id);
        }

        public async Task<UserTelegramInformation> GetUserTgInfoFromTgId(int tgId)
        {
            return await userTelegramInformationRepository.GetUserTgInfoFromTgId(tgId);
        }

        public async Task UpdateAsync(UserTelegramInformation userInfo)
        {
            userTelegramInformationRepository.UpdateAsync(userInfo);
            await userTelegramInformationRepository.SaveChangesAsync();
        }
    }
}
