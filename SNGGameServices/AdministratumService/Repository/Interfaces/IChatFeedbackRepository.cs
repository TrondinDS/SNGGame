using AdministratumService.DB.Models;
using Library.GenericRepository.Interfaces;

namespace AdministratumService.Repository.Interfaces
{
    public interface IChatFeedbackRepository : IGenericRepository<ChatFeedback, int>
    {
    }
}