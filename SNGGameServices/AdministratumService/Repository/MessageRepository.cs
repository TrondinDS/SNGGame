using AdministratumService.DB.Context;
using AdministratumService.DB.Models;
using AdministratumService.Filter;
using AdministratumService.Repository.Interfaces;
using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.Administratum;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Repository;

public class MessageRepository : GenericRepository<Message, Guid>, IMessageRepository
{
    private readonly DbSet<Message> dbSet;

    public MessageRepository(ApplicationContext context) : base(context)
    {
        dbSet = context.Set<Message>();
    }
    public async Task<IEnumerable<Message>> Filter(ParamQueryMessage param)
    {
        var query = dbSet.AsQueryable();

        query = FilterQueryMessage.CreateQuerybleAsNoTracking(param, query);
        var result = await query.ToListAsync();

        return result;
    }

}
