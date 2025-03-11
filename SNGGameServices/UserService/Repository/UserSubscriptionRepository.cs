using Library.GenericRepository;
using UserService.DB.Context;
using UserService.DB.Models;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class UserSubscriptionRepository : GenericRepository<UserSubscription, int>,IUserSubscriptionRepository
    {
        UserSubscriptionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
