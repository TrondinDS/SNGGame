using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameSelectedGenreService
    {
        Task AddAsync(GameSelectedGenre gameSelectedGenre);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GameSelectedGenre>> GetAllAsync();
        Task<GameSelectedGenre> GetByIdAsync(Guid id);
        Task UpdateAsync(GameSelectedGenre gameSelectedGenre);
    }
}
