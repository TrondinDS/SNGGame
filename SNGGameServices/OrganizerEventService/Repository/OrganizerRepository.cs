using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.EntityFrameworkCore;
using OrganizerEventService.DB.Context;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Filter;
using OrganizerEventService.Repository.Interfaces;

namespace OrganizerEventService.Repository
{
    public class OrganizerRepository : GenericRepository<Organizer, Guid>, IOrganizerRepository
    {
        private readonly DbSet<Organizer> dbSet;

        public OrganizerRepository(ApplicationContext context) : base(context)
        {
            dbSet = context.Set<Organizer>();
        }

        public async Task<IEnumerable<Organizer>> Filter(ParamQueryOrganizer param)
        {
            var query = dbSet.AsQueryable();

            query = FilterQueryOrganizer.CreateQuerybleAsNoTracking(param, query);
            var result = await query.ToListAsync();

            return result;
        }

    }
}
