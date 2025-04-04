using AdministratumService.DB.Context;
using AdministratumService.DB.Models;
using AdministratumService.Repository.Interfaces;
using Library.Generics.GenericRepository;

namespace AdministratumService.Repository
{
    public class MessageRepository : GenericRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(ApplicationContext context) : base(context) { }

    }
}
