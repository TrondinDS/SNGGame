using UserActivityService.DB.Models;

namespace UserActivityService.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddAsync(Comment comment);
        Task DeleteAsync(int id);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task UpdateAsync(Comment comment);
    }
}
