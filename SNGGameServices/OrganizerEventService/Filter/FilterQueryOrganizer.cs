using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.EntityFrameworkCore;
using OrganizerEventService.Filter.Organizer;

namespace OrganizerEventService.Filter
{
    public class FilterQueryOrganizer
    {
        public static IQueryable<DB.Models.Organizer> CreateQuerybleAsNoTracking(
            ParamQueryOrganizer param,
            IQueryable<DB.Models.Organizer> bodyQuery
        )
        {
            bodyQuery = bodyQuery.AsNoTracking();
            bodyQuery = CreateQueryble(param, bodyQuery);
            return bodyQuery;
        }

        private static IQueryable<DB.Models.Organizer> CreateQueryble(
            ParamQueryOrganizer param,
            IQueryable<DB.Models.Organizer> bodyQuery
        )
        {
            bodyQuery = OrganizerOrganizerQuery.Create(param.QueryOrganizer, bodyQuery);
            return bodyQuery;
        }
    }
}
