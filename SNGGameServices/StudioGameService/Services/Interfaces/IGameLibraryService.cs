using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameLibraryService
    {
        Task AddAsync(GameLibrary gameLibrary);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GameLibrary>> GetAllAsync();
        Task<GameLibrary> GetByIdAsync(Guid id);
        Task UpdateAsync(GameLibrary gameLibrary);
    }
}
