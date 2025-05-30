using GetAwaitService.DB.Models;

namespace GetAwaitService.Services.GetAwaitService.Interfaces
{
    public interface IUserTelegramInformationService
    {
        Task AddAsync(UserTelegramInformation userInfo);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<UserTelegramInformation>> GetAllAsync();
        Task<UserTelegramInformation> GetByIdAsync(Guid id);
        Task UpdateAsync(UserTelegramInformation userInfo);
        public Task<UserTelegramInformation> GetUserTgInfoFromTgId(int tgId);
    }
}
