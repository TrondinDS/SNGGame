using Library.Generics.Query.QueryModels.Administratum.ChatFeedback;
using Library.Generics.Query.QueryModels.OrganizerEvent.Event;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Filter.Chatfeedback;

public class EventChatfeedbackChatfeedback
{
    public static IQueryable<DB.Models.ChatFeedback> Create(QueryChatfeedbackChatfeedback query, IQueryable<DB.Models.ChatFeedback> bodyQuery)
    {
        if (query == null || bodyQuery == null) return bodyQuery;

        if (query.ChatfeedbackId is not null)
            bodyQuery = bodyQuery.Where(x => query.ChatfeedbackId.Contains(x.Id));

        if (!string.IsNullOrEmpty(query.Title))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Title, $"%{query.Title}%"));

        if (query.DatetimeBegin != default && query.DatetimeEnd != default)
        {
            bodyQuery = bodyQuery.Where(x => x.Date >= query.DatetimeBegin && x.Date <= query.DatetimeEnd);
        } else if (query.DatetimeBegin != default)
        {
            bodyQuery = bodyQuery.Where(x => x.Date >= query.DatetimeBegin);
        } else if (query.DatetimeEnd != default)
        {
            bodyQuery = bodyQuery.Where(x => x.Date <= query.DatetimeEnd);
        }

        if (query.UserIds is not null)
            bodyQuery = bodyQuery.Where(x => query.UserIds.Contains(x.Id));

        if (query.MessageIds is not null)
            bodyQuery = bodyQuery.Where(x => query.MessageIds.Contains(x.Id));

        return bodyQuery;
    }
}
