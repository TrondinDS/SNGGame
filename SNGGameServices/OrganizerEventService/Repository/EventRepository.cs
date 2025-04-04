using Library.Generics.GenericRepository;
using OrganizerEventService.DB.Context;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Repository.Interfaces;

namespace OrganizerEventService.Repository
{
    public class EventRepository : GenericRepository<Event, Guid>, IEventRepository
    {
        public EventRepository(ApplicationContext context) : base(context) { }
    }
}
