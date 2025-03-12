using UserService.DB.Models;
using UserService.Repository.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        protected readonly IUserSubscriptionRepository userSubscriptionRepository;

        public UserSubscriptionService(IUserSubscriptionRepository userSubscriptionRepository)
        {
            this.userSubscriptionRepository = userSubscriptionRepository;
        }

        public async Task AddAsync(UserSubscription userSub)
        {
            await userSubscriptionRepository.AddAsync(userSub);
            await userSubscriptionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userSub = await userSubscriptionRepository.GetByIdAsync(id);
            if (userSub != null)
            {
                userSubscriptionRepository.DeleteAsync(userSub);
                await userSubscriptionRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<UserSubscription>> GetAllAsync()
        {
            return userSubscriptionRepository.GetAllAsync();
        }

        public async Task<UserSubscription> GetByIdAsync(int id)
        {
            return await userSubscriptionRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserSubscription userSub)
        {
            userSubscriptionRepository.UpdateAsync(userSub);
            await userSubscriptionRepository.SaveChangesAsync();
        }
    }
}
