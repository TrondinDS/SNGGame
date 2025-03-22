using Library.Generics.GenericRepository;
using UserActivityService.DB.Context;
using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;

namespace UserActivityService.Repository
{
    public class TopicRepository : GenericRepository<Topic, int>, ITopicRepository 
    {
        public TopicRepository(ApplicationContext context) : base(context)
        { }
    }
}
