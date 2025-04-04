using Library.Generics.GenericRepository;
using OrganizerEventService.DB.Context;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Repository.Interfaces;

namespace OrganizerEventService.Repository
{
    public class OrganizerRepository : GenericRepository<Organizer, Guid>, IOrganizerRepository
    {
        public OrganizerRepository(ApplicationContext context) : base(context) { }
    }
}
