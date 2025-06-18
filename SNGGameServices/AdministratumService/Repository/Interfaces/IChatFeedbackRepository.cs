using AdministratumService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.Administratum;

namespace AdministratumService.Repository.Interfaces;

public interface IChatFeedbackRepository : IGenericRepository<ChatFeedback, Guid>
{
    Task<IEnumerable<ChatFeedback>> Filter(ParamQueryChatfeedback param);

}