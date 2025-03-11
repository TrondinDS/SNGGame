using BannedService.DB.Models;
using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IBannedService
    {
        Task AddAsync(Banned banned);
        Task DeleteAsync(int id);
        Task<IEnumerable<Banned>> GetAllAsync();
        Task<Banned> GetByIdAsync(int id);
        Task UpdateAsync(Banned banned);
    }
}
