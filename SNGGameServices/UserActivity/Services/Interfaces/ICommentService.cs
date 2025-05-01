using UserActivityService.DB.Models;

namespace UserActivityService.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddAsync(Comment comment);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(Guid id);
        Task UpdateAsync(Comment comment);
    }
}
