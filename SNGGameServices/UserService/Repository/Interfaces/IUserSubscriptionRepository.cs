using Library.Generics.GenericRepository.Interfaces;
using UserService.DB.Models;

namespace UserService.Repository.Interfaces
{
    public interface IUserSubscriptionRepository : IGenericRepository<UserSubscription, int>
    {
        // TODO : Create abstract method IUSRepository
    }
}
