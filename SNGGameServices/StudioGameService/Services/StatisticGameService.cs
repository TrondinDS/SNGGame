using StudioGameService.DB.Model;
using StudioGameService.Repository;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class StatisticGameService : IStatisticGameService
    {
        protected readonly IStatisticGameRepository statisticGameRepository;

        public StatisticGameService(IStatisticGameRepository statisticGameRepository)
        {
            this.statisticGameRepository = statisticGameRepository;
        }

        public async Task AddAsync(StatisticGame statisticGame)
        {
            await statisticGameRepository.AddAsync(statisticGame);
            await statisticGameRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var Library = await statisticGameRepository.GetByIdAsync(id);
            if (Library != null)
            {
                statisticGameRepository.DeleteAsync(Library);
                await statisticGameRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<StatisticGame>> GetAllAsync()
        {
            return statisticGameRepository.GetAllAsync();
        }

        public async Task<StatisticGame> GetByIdAsync(Guid id)
        {
            return await statisticGameRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(StatisticGame statisticGame)
        {
            statisticGameRepository.UpdateAsync(statisticGame);
            await statisticGameRepository.SaveChangesAsync();
        }
    }
}
