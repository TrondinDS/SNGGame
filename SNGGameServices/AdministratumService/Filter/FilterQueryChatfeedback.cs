using AdministratumService.Filter.Chatfeedback;
using Library.Generics.Query.QueryModels.Administratum;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.Filter;

public class FilterQueryChatfeedback
{
    public static IQueryable<DB.Models.ChatFeedback> CreateQuerybleAsNoTracking(
        ParamQueryChatfeedback param,
        IQueryable<DB.Models.ChatFeedback> bodyQuery
    )
    {
        bodyQuery = bodyQuery.AsNoTracking();
        bodyQuery = CreateQueryble(param, bodyQuery);
        return bodyQuery;
    }

    private static IQueryable<DB.Models.ChatFeedback> CreateQueryble(
        ParamQueryChatfeedback param,
        IQueryable<DB.Models.ChatFeedback> bodyQuery
    )
    {
        bodyQuery = ChatfeedbackChatfeedbackQuery.Create(param.QueryChatfeedback, bodyQuery);
        return bodyQuery;
    }
}
