using Library.GenericRepository.Interfaces;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        // TODO : Create abstract method in interface UserRepository
    }
}
