using GetAwaitService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;

namespace GetAwaitService.Repository
{
    public interface IUserTelegramInformationRepository : IGenericRepository<UserTelegramInformation, int>
    {
        public Task<UserTelegramInformation> GetUserTgInfoFromTgId(int tgId);
    }
}
