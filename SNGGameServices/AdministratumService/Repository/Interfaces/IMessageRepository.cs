using AdministratumService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;

namespace AdministratumService.Repository.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message, int>
    {
    }
}
