using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;
using UserActivityService.Services.Interfaces;

namespace UserActivityService.Services
{
    public class UserReactionService : IUserReactionService
    {
        protected readonly IUserReactionRepository userReactionRepository;

        public UserReactionService(IUserReactionRepository userReactionRepository)
        {
            this.userReactionRepository = userReactionRepository;
        }

        public async Task AddAsync(UserReaction userReaction)
        {
            await userReactionRepository.AddAsync(userReaction);
            await userReactionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userReaction = await userReactionRepository.GetByIdAsync(id);
            if (userReaction != null)
            {
                userReactionRepository.DeleteAsync(userReaction);
                await userReactionRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<UserReaction>> GetAllAsync()
        {
            return userReactionRepository.GetAllAsync();
        }

        public async Task<UserReaction> GetByIdAsync(int id)
        {
            return await userReactionRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserReaction userReaction)
        {
            userReactionRepository.UpdateAsync(userReaction);
            await userReactionRepository.SaveChangesAsync();
        }
    }
}
