using UserActivityService.DB.Models;

namespace UserActivityService.Services.Interfaces
{
    public interface ITopicService
    {
        Task AddAsync(Topic topic);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<Topic> GetByIdAsync(Guid id);
        Task UpdateAsync(Topic topic);

        public Task<IEnumerable<Topic>> GetTopicsByEntityIdAsync(List<Guid> entityIds);
    }
}
