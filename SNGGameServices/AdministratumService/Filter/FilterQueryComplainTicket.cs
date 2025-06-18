using AdministratumService.Filter.Chatfeedback;
using AdministratumService.Filter.ComplainTicket;
using Library.Generics.Query.QueryModels.Administratum;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Filter;

public class FilterQueryComplainTicket
{
    public static IQueryable<DB.Models.ComplainTicket> CreateQuerybleAsNoTracking(
    ParamQueryComplainTicket param,
    IQueryable<DB.Models.ComplainTicket> bodyQuery
)
    {
        bodyQuery = bodyQuery.AsNoTracking();
        bodyQuery = CreateQueryble(param, bodyQuery);
        return bodyQuery;
    }

    private static IQueryable<DB.Models.ComplainTicket> CreateQueryble(
        ParamQueryComplainTicket param,
        IQueryable<DB.Models.ComplainTicket> bodyQuery
    )
    {
        bodyQuery = ComplainTicketComplainTicketQuery.Create(param.ComplainTicket, bodyQuery);
        return bodyQuery;
    }
}
