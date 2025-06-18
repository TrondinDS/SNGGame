using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.EntityFrameworkCore;
using OrganizerEventService.DB.Context;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Filter;
using OrganizerEventService.Repository.Interfaces;

namespace OrganizerEventService.Repository
{
    public class EventRepository : GenericRepository<Event, Guid>, IEventRepository
    {
        private readonly DbSet<Event> dbSet;

        public EventRepository(ApplicationContext context) : base(context)
        {
            dbSet = context.Set<Event>();
        }

        public async Task<IEnumerable<Event>> Filter(ParamQueryEvent param)
        {
            var query = dbSet.AsQueryable();

            query = FilterQueryEvent.CreateQuerybleAsNoTracking(param, query);
            var result = await query.ToListAsync();

            return result;
        }
    }
}
