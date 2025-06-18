using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.EntityFrameworkCore;
using OrganizerEventService.Filter.Event;

namespace OrganizerEventService.Filter;

public class FilterQueryEvent
{
    public static IQueryable<DB.Models.Event> CreateQuerybleAsNoTracking(
        ParamQueryEvent param,
        IQueryable<DB.Models.Event> bodyQuery
    )
    {
        bodyQuery = bodyQuery.AsNoTracking();
        bodyQuery = CreateQueryble(param, bodyQuery);
        return bodyQuery;
    }

    private static IQueryable<DB.Models.Event> CreateQueryble(
        ParamQueryEvent param,
        IQueryable<DB.Models.Event> bodyQuery
    )
    {
        bodyQuery = EventQuery.Create(param.QueryEvent, bodyQuery);
        bodyQuery = OrganizerQuery.Create(param.QueryOrganizer, bodyQuery);
        return bodyQuery;
    }
}
