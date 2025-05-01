using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserSubscriptionService
    {
        Task AddAsync(UserSubscription userSubscription);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<UserSubscription>> GetAllAsync();
        Task<UserSubscription> GetByIdAsync(Guid id);
        Task UpdateAsync(UserSubscription userSubscription);
    }
}
