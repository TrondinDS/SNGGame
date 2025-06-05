using Library.Generics.GenericRepository.Interfaces;
using StudioGameService.DB.Model;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job, Guid>
    {
        public Task<IEnumerable<Job>> GetJobsByUserIdAsync(Guid id);
    }
}
