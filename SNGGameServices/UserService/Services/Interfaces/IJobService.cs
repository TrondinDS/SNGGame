using StudioGameService.DB.Model;
using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IJobService
    {
        Task AddAsync(Job job);
        Task DeleteAsync(int id);
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job> GetByIdAsync(int id);
        Task UpdateAsync(Job job);
    }
}
