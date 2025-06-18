using AdministratumService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.Administratum;

namespace AdministratumService.Repository.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message, Guid>
    {
        Task<IEnumerable<Message>> Filter(ParamQueryMessage param);
    }
}
