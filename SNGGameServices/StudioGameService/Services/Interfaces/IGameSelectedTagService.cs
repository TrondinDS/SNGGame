using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameSelectedTagService
    {
        Task AddAsync(GameSelectedTag gameSelectedTag);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GameSelectedTag>> GetAllAsync();
        Task<GameSelectedTag> GetByIdAsync(Guid id);
        Task UpdateAsync(GameSelectedTag gameSelectedTag);
    }
}
