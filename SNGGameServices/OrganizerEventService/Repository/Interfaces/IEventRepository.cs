using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using OrganizerEventService.DB.Models;

namespace OrganizerEventService.Repository.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event, Guid>
    {
        Task<IEnumerable<Event>> Filter(ParamQueryEvent param);
    }
}
