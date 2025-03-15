using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameSelectedGenreService
    {
        Task AddAsync(GameSelectedGenre gameSelectedGenre);
        Task DeleteAsync(int id);
        Task<IEnumerable<GameSelectedGenre>> GetAllAsync();
        Task<GameSelectedGenre> GetByIdAsync(int id);
        Task UpdateAsync(GameSelectedGenre gameSelectedGenre);
    }
}
