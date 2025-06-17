using Library.Generics.Query.QueryModels.OrganizerEvent.Organizer;

namespace OrganizerEventService.Filter.Event
{
    public class OrganizerQuery
    {
        public static IQueryable<DB.Models.Event> Create(QueryOrganizer query, IQueryable<DB.Models.Event> bodyQuery)
        {
            if (query == null || bodyQuery == null) return bodyQuery;

            if (query.OrganizerId is not null)
                bodyQuery = bodyQuery.Where(x => query.OrganizerId.Contains(x.OrganizerEventId));

            return bodyQuery;
        }
    }
}
