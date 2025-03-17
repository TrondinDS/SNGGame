using Library.GenericRepository.Interfaces;
using UserActivityService.DB.Models;

namespace UserActivityService.Repository.Interfaces
{
    public interface ITopicRepository : IGenericRepository<Topic, int>
    {
    }
}
