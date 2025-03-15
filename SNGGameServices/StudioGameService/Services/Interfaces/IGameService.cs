using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameService
    {
        Task AddAsync(Game game);
        Task DeleteAsync(int id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(int id);
        Task UpdateAsync(Game game);
    }
}
