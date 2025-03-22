using Library.Generics.GenericRepository.Interfaces;
using OrganizerEventService.DB.Models;

namespace OrganizerEventService.Repository.Interfaces
{
    public interface IOrganizerRepository : IGenericRepository<Organizer, int>
    {
    }
}
