using UserActivityService.DB.Models;

namespace UserActivityService.Services.Interfaces
{
    public interface ITopicService
    {
        Task AddAsync(Topic topic);
        Task DeleteAsync(int id);
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<Topic> GetByIdAsync(int id);
        Task UpdateAsync(Topic topic);
    }
}
