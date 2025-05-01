using StudioGameService.DB.Model;
using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IJobService
    {
        Task AddAsync(Job job);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job> GetByIdAsync(Guid id);
        Task UpdateAsync(Job job);
    }
}
