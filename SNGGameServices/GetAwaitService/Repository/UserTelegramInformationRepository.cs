using GetAwaitService.DB.Context;
using GetAwaitService.DB.Models;
using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace GetAwaitService.Repository
{
    public class UserTelegramInformationRepository : GenericRepository<UserTelegramInformation, Guid>, IUserTelegramInformationRepository
    {
        private readonly DbSet<UserTelegramInformation> _userTgInfo;
        public UserTelegramInformationRepository(ApplicationContext context)
            : base(context) 
        {
            _userTgInfo = context.Set<UserTelegramInformation>();
        }

        public async Task<UserTelegramInformation> GetUserByIdUser(Guid userId)
        {
            return await _userTgInfo.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<UserTelegramInformation> GetUserTgInfoFromTgId(ulong tgId)
        {
            return await _userTgInfo.FirstOrDefaultAsync(u => u.TelegramId == tgId);
        }
    }
}
