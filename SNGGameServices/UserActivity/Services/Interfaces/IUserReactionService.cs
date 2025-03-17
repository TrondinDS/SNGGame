using UserActivityService.DB.Models;

namespace UserActivityService.Services.Interfaces
{
    public interface IUserReactionService
    {
        Task AddAsync(UserReaction userReaction);
        Task DeleteAsync(int id);
        Task<IEnumerable<UserReaction>> GetAllAsync();
        Task<UserReaction> GetByIdAsync(int id);
        Task UpdateAsync(UserReaction userReaction);
    }
}
