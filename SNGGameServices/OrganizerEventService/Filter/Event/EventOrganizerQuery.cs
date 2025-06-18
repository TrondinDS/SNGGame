using Library.Generics.Query.QueryModels.OrganizerEvent.Event;

namespace OrganizerEventService.Filter.Event
{
    public class EventOrganizerQuery
    {
        public static IQueryable<DB.Models.Event> Create(QueryEventOrganizer query, IQueryable<DB.Models.Event> bodyQuery)
        {
            if (query == null || bodyQuery == null) return bodyQuery;

            if (query.OrganizerId is not null)
                bodyQuery = bodyQuery.Where(x => query.OrganizerId.Contains(x.OrganizerEventId));

            return bodyQuery;
        }
    }
}
