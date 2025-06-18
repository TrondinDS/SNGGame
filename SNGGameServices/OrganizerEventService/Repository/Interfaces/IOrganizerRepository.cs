using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using OrganizerEventService.DB.Models;

namespace OrganizerEventService.Repository.Interfaces
{
    public interface IOrganizerRepository : IGenericRepository<Organizer, Guid>
    {
        Task<IEnumerable<Organizer>> Filter(ParamQueryOrganizer param);
    }
}
