using AdministratumService.DB.Context;
using AdministratumService.DB.Models;
using AdministratumService.Repository.Interfaces;
using Library.Generics.GenericRepository;

namespace AdministratumService.Repository
{
    public class ChatFeedbackRepository : GenericRepository<ChatFeedback, Guid>, IChatFeedbackRepository
    {
        public ChatFeedbackRepository(ApplicationContext context) : base(context) { }
    }
}
