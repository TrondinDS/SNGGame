using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameSelectedTagService
    {
        Task AddAsync(GameSelectedTag gameSelectedTag);
        Task DeleteAsync(int id);
        Task<IEnumerable<GameSelectedTag>> GetAllAsync();
        Task<GameSelectedTag> GetByIdAsync(int id);
        Task UpdateAsync(GameSelectedTag gameSelectedTag);
    }
}
