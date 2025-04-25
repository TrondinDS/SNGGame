using Library.Generics.GenericRepository.Interfaces;
using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IStatisticGameService 
    {
        Task AddAsync(StatisticGame statisticGame);
        Task DeleteAsync(int id);
        Task<IEnumerable<StatisticGame>> GetAllAsync();
        Task<StatisticGame> GetByIdAsync(int id);
        Task UpdateAsync(StatisticGame statisticGame);
    }
}
