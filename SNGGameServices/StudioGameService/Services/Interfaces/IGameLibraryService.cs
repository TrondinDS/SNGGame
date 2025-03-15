using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameLibraryService
    {
        Task AddAsync(GameLibrary gameLibrary);
        Task DeleteAsync(int id);
        Task<IEnumerable<GameLibrary>> GetAllAsync();
        Task<GameLibrary> GetByIdAsync(int id);
        Task UpdateAsync(GameLibrary gameLibrary);
    }
}
