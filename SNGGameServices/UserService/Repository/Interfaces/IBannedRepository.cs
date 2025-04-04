using BannedService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IBannedRepository : IGenericRepository<Banned, int>
    {
        // TODO : Create abstract method IBRepository
    }
}
