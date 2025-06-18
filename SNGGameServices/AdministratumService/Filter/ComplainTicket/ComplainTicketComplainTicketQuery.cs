using Library.Generics.Query.QueryModels.Administratum.ComplainTicket;
using Library.Generics.Query.QueryModels.Administratum.Message;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Filter.ComplainTicket;

public class ComplainTicketComplainTicketQuery
{
    public static IQueryable<DB.Models.ComplainTicket> Create(QueryComplainTicketComplainTicket query, IQueryable<DB.Models.ComplainTicket> bodyQuery)
    {
        if (query == null || bodyQuery == null) return bodyQuery;

        if (query.ComplainTicketIds is not null)
            bodyQuery = bodyQuery.Where(x => query.ComplainTicketIds.Contains(x.Id));

        if (query.EntityIds is not null)
            bodyQuery = bodyQuery.Where(x => query.EntityIds.Contains(x.Id));
        
        if (query.EntityTypes is not null)
            bodyQuery = bodyQuery.Where(x => query.EntityTypes.Contains(x.EntityType));

        if (query.ComplainTypes is not null)
            bodyQuery = bodyQuery.Where(x => query.ComplainTypes.Contains(x.ComplainType));

        if (query.Statuses is not null)
            bodyQuery = bodyQuery.Where(x => query.Statuses.Contains(x.Status));

        if (query.DatetimeBegin != default && query.DatetimeEnd != default)
        {
            bodyQuery = bodyQuery.Where(x => x.StatusUpdateDatetime >= query.DatetimeBegin && x.StatusUpdateDatetime <= query.DatetimeEnd);
        }
        else if (query.DatetimeBegin != default)
        {
            bodyQuery = bodyQuery.Where(x => x.StatusUpdateDatetime >= query.DatetimeBegin);
        }
        else if (query.DatetimeEnd != default)
        {
            bodyQuery = bodyQuery.Where(x => x.StatusUpdateDatetime <= query.DatetimeEnd);
        }

        return bodyQuery;
    }
}
