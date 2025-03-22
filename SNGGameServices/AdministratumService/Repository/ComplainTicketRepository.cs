using AdministratumService.DB.Context;
using AdministratumService.DB.Models;
using AdministratumService.Repository.Interfaces;
using Library.Generics.GenericRepository;

namespace AdministratumService.Repository
{
    public class ComplainTicketRepository : GenericRepository<ComplainTicket, int>, IComplainTicketRepository
    {
        public ComplainTicketRepository(ApplicationContext context) : base(context) { }

    }
}
