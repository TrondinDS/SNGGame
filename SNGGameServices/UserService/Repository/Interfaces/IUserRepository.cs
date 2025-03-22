using Library.Generics.GenericRepository.Interfaces;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        // TODO : Create abstract method in interface UserRepository
    }
}
