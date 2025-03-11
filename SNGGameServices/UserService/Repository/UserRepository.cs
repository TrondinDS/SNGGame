using Library.GenericRepository;
using UserService.DB.Context;
using UserService.DB.Models;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
