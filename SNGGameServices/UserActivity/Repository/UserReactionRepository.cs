using Library.Generics.GenericRepository;
using UserActivityService.DB.Context;
using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;

namespace UserActivityService.Repository
{
    public class UserReactionRepository : GenericRepository<UserReaction, int>, IUserReactionRepository
    {
        public UserReactionRepository(ApplicationContext context) : base(context)
        { }
    }
}
