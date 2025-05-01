using UserActivityService.DB.Models;

namespace UserActivityService.Services.Interfaces
{
    public interface IUserReactionService
    {
        Task AddAsync(UserReaction userReaction);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<UserReaction>> GetAllAsync();
        Task<UserReaction> GetByIdAsync(Guid id);
        Task UpdateAsync(UserReaction userReaction);
    }
}
