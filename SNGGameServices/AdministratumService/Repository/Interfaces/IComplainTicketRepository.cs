using AdministratumService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.Administratum;

namespace AdministratumService.Repository.Interfaces
{
    public interface IComplainTicketRepository : IGenericRepository<ComplainTicket, Guid>
    {
        Task<IEnumerable<ComplainTicket>> Filter(ParamQueryComplainTicket param);
    }
}