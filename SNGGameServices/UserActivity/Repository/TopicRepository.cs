using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserActivityService.DB.Context;
using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;

namespace UserActivityService.Repository
{
    public class TopicRepository : GenericRepository<Topic, int>, ITopicRepository 
    {
        private DbSet<Topic> topicDbSet;
        public TopicRepository(ApplicationContext context) : base(context)
        {
            topicDbSet = context.Set<Topic>();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByEntityIdAsync(List<Guid> entityIds)
        {
            if (entityIds == null || entityIds.Count == 0)
                return new List<Topic>();

            return await topicDbSet
                .Include(t => t.Comments)
                .Where(t => entityIds.Contains(t.EntityId))
                .ToListAsync();
        }
    }
}
