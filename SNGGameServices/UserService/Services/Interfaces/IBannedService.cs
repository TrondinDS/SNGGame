using BannedService.DB.Models;
using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IBannedService
    {
        Task AddAsync(Banned banned);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Banned>> GetAllAsync();
        Task<Banned> GetByIdAsync(Guid id);
        Task UpdateAsync(Banned banned);
        public Task<IEnumerable<Banned>> GetBannedsByUserIdAsync(Guid userId);
    }
}
