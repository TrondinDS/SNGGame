using AdministratumService.DB.Context;
using AdministratumService.DB.Models;
using AdministratumService.Filter;
using AdministratumService.Repository.Interfaces;
using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.Administratum;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Repository
{
    public class ComplainTicketRepository : GenericRepository<ComplainTicket, Guid>, IComplainTicketRepository
    {
        private readonly DbSet<ComplainTicket> dbSet;

        public ComplainTicketRepository(ApplicationContext context) : base(context)
        {
            dbSet = context.Set<ComplainTicket>();
        }

        public async Task<IEnumerable<ComplainTicket>> Filter(ParamQueryComplainTicket param)
        {
            var query = dbSet.AsQueryable();

            query = FilterQueryComplainTicket.CreateQuerybleAsNoTracking(param, query);
            var result = await query.ToListAsync();

            return result;
        }
    }
}
