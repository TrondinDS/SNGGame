using Library.Generics.Query.QueryModels.OrganizerEvent.Organizer;
using Microsoft.EntityFrameworkCore;

namespace OrganizerEventService.Filter.Organizer;

public class OrganizerOrganizerQuery
{
    public static IQueryable<DB.Models.Organizer> Create(QueryOrganizerOrganizer query, IQueryable<DB.Models.Organizer> bodyQuery)
    {
        if (query == null || bodyQuery == null) return bodyQuery;

        if (query.OrganizerId is not null)
            bodyQuery = bodyQuery.Where(x => query.OrganizerId.Contains(x.Id));

        if (!string.IsNullOrEmpty(query.Title))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Title, $"%{query.Title}%"));

        if (!string.IsNullOrEmpty(query.Mail))
            bodyQuery = bodyQuery.Where(x => x.Mail.Equals(query.Mail, StringComparison.OrdinalIgnoreCase));

        if (query.CreatorId is not null)
            bodyQuery = bodyQuery.Where(x => query.CreatorId.Contains(x.Id));

        if (query.OwnerId is not null)
            bodyQuery = bodyQuery.Where(x => query.OwnerId.Contains(x.Id));

        return bodyQuery;
    }
}
