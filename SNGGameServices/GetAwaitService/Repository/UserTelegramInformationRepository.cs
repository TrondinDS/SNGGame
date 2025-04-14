using GetAwaitService.DB.Context;
using GetAwaitService.DB.Models;
using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace GetAwaitService.Repository
{
    public class UserTelegramInformationRepository : GenericRepository<UserTelegramInformation, int>, IUserTelegramInformationRepository
    {
        private readonly DbSet<UserTelegramInformation> _userTgInfo;
        public UserTelegramInformationRepository(ApplicationContext context)
            : base(context) 
        {
            _userTgInfo = context.Set<UserTelegramInformation>();
        }

        public async Task<UserTelegramInformation> GetUserTgInfoFromTgId(int tgId)
        {
            return await _userTgInfo.FirstOrDefaultAsync(u => u.TelegramId == tgId);
        }
    }
}
