using Library.Generics.Query.QueryModels.OrganizerEvent.Event;
using Microsoft.EntityFrameworkCore;

namespace OrganizerEventService.Filter.Event;

public class EventQuery
{
    public static IQueryable<DB.Models.Event> Create(QueryEvent query, IQueryable<DB.Models.Event> bodyQuery)
    {
        if (query == null || bodyQuery == null) return bodyQuery;

        if (query.EventId is not null)
            bodyQuery = bodyQuery.Where(x => query.EventId.Contains(x.Id));

        if (!string.IsNullOrEmpty(query.Title))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Title, $"%{query.Title}%"));

        if (!string.IsNullOrEmpty(query.Address))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Address, $"%{query.Address}%"));

        if (!string.IsNullOrEmpty(query.Country))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Country, $"%{query.Country}%"));

        if (!string.IsNullOrEmpty(query.Region))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Region, $"%{query.Region}%"));

        if (!string.IsNullOrEmpty(query.City))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.City, $"%{query.City}%"));

        if (!string.IsNullOrEmpty(query.GeoUrl))
            bodyQuery = bodyQuery.Where(x => x.GeoUrl.Equals(query.GeoUrl, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(query.Status))
            bodyQuery = bodyQuery.Where(x => EF.Functions.ILike(x.Status, $"%{query.Status}%"));

        if (
            query.PriceMin.HasValue && query.PriceMin >= 0 &&
            query.PriceMax.HasValue && query.PriceMax >= 0 &&
            query.PriceMax >= query.PriceMin
        )
        {
            bodyQuery = bodyQuery.Where(x => x.PriceMin >= query.PriceMin.Value && x.PriceMax <= query.PriceMax.Value);
        } else if (query.PriceMin.HasValue && query.PriceMin >= 0)
        {
            bodyQuery = bodyQuery.Where(x => x.PriceMin >= query.PriceMin.Value);
        } else if (query.PriceMax.HasValue && query.PriceMax >= 0)
        {
            bodyQuery = bodyQuery.Where(x => x.PriceMax <= query.PriceMax.Value);
        }

        return bodyQuery;
    }
}
