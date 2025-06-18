using Library.Generics.Query.QueryModels.Administratum.ChatFeedback;
using Library.Generics.Query.QueryModels.Administratum.Message;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Filter.Message;

public class MessageMessageQuery
{
    public static IQueryable<DB.Models.Message> Create(QueryMessageMessage query, IQueryable<DB.Models.Message> bodyQuery)
    {
        if (query == null || bodyQuery == null) return bodyQuery;

        if (query.MessageIds is not null)
            bodyQuery = bodyQuery.Where(x => query.MessageIds.Contains(x.Id));
        
        if (query.UserIds is not null)
            bodyQuery = bodyQuery.Where(x => query.UserIds.Contains(x.Id));

        if (!string.IsNullOrEmpty(query.Content))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Content, $"%{query.Content}%"));

        if (query.DatetimeBegin != default && query.DatetimeEnd != default)
        {
            bodyQuery = bodyQuery.Where(x => x.Date >= query.DatetimeBegin && x.Date <= query.DatetimeEnd);
        }
        else if (query.DatetimeBegin != default)
        {
            bodyQuery = bodyQuery.Where(x => x.Date >= query.DatetimeBegin);
        }
        else if (query.DatetimeEnd != default)
        {
            bodyQuery = bodyQuery.Where(x => x.Date <= query.DatetimeEnd);
        }

        if (query.ChatfeedbackIds is not null)
            bodyQuery = bodyQuery.Where(x => query.ChatfeedbackIds.Contains(x.Id));

        return bodyQuery;
    }
}
