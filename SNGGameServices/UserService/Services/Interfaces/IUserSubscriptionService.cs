using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserSubscriptionService
    {
        Task AddAsync(UserSubscription userSubscription);
        Task DeleteAsync(int id);
        Task<IEnumerable<UserSubscription>> GetAllAsync();
        Task<UserSubscription> GetByIdAsync(int id);
        Task UpdateAsync(UserSubscription userSubscription);
    }
}
