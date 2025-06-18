using AdministratumService.Filter.Chatfeedback;
using AdministratumService.Filter.Message;
using Library.Generics.Query.QueryModels.Administratum;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Filter;

public class FilterQueryMessage
{
    public static IQueryable<DB.Models.Message> CreateQuerybleAsNoTracking(
        ParamQueryMessage param,
        IQueryable<DB.Models.Message> bodyQuery
    )
    {
        bodyQuery = bodyQuery.AsNoTracking();
        bodyQuery = CreateQueryble(param, bodyQuery);
        return bodyQuery;
    }

    private static IQueryable<DB.Models.Message> CreateQueryble(
        ParamQueryMessage param,
        IQueryable<DB.Models.Message> bodyQuery
    )
    {
        bodyQuery = EventMessageMessage.Create(param.QueryMessage, bodyQuery);
        return bodyQuery;
    }
}
