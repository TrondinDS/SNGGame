using Library.Generics.GenericRepository.Interfaces;
using UserActivityService.DB.Models;

namespace UserActivityService.Repository.Interfaces
{
    public interface ITopicRepository : IGenericRepository<Topic, Guid>
    {
        public Task<IEnumerable<Topic>> GetTopicsByEntityIdAsync(List<Guid> entityIds);
    }
}
