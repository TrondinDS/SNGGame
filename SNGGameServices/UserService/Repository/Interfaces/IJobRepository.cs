using Library.GenericRepository.Interfaces;
using StudioGameService.DB.Model;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job, int>
    {
        // TODO : Create abstract method IJRepository
    }
}
