using GetAwaitService.DB.Models;

namespace GetAwaitService.Services.Interfaces
{
    public interface IUserTelegramInformationService
    {
        Task AddAsync(UserTelegramInformation userInfo);
        Task DeleteAsync(int id);
        Task<IEnumerable<UserTelegramInformation>> GetAllAsync();
        Task<UserTelegramInformation> GetByIdAsync(int id);
        Task UpdateAsync(UserTelegramInformation userInfo);
        public Task<UserTelegramInformation> GetUserTgInfoFromTgId(int tgId);
    }
}
