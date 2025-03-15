using BannedService.DB.Models;
using UserService.Repository.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class BannedServiceS : IBannedService
    {
        protected readonly IBannedRepository bannedRepository;

        public BannedServiceS(IBannedRepository bannedRepository)
        {
            this.bannedRepository = bannedRepository;
        }

        public async Task AddAsync(Banned banned)
        {
            await bannedRepository.AddAsync(banned);
            await bannedRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await bannedRepository.GetByIdAsync(id);
            if (user != null)
            {
                bannedRepository.DeleteAsync(user);
                await bannedRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Banned>> GetAllAsync()
        {
            return bannedRepository.GetAllAsync();
        }

        public async Task<Banned> GetByIdAsync(int id)
        {
            return await bannedRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Banned banned)
        {
            bannedRepository.UpdateAsync(banned);
            await bannedRepository.SaveChangesAsync();
        }
    }
}
