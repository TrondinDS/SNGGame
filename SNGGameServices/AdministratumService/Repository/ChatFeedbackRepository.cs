using AdministratumService.DB.Context;
using AdministratumService.DB.Models;
using AdministratumService.Filter;
using AdministratumService.Repository.Interfaces;
using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.Administratum;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Repository;

public class ChatFeedbackRepository : GenericRepository<ChatFeedback, Guid>, IChatFeedbackRepository
{
    private readonly DbSet<ChatFeedback> dbSet;

    public ChatFeedbackRepository(ApplicationContext context) : base(context)
    {
        dbSet = context.Set<ChatFeedback>();
    }

    public async Task<IEnumerable<ChatFeedback>> Filter(ParamQueryChatfeedback param)
    {
        var query = dbSet.AsQueryable();

        query = FilterQueryChatfeedback.CreateQuerybleAsNoTracking(param, query);
        var result = await query.ToListAsync();

        return result;
    }
}
