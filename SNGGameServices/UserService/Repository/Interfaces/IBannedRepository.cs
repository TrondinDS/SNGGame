using BannedService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IBannedRepository : IGenericRepository<Banned, Guid>
    {
        public Task<IEnumerable<Banned>> GetBannedsByUserIdAsync(Guid userId);
    }
}
