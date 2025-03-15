using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGenreService
    {
        Task AddAsync(Genre genre);
        Task DeleteAsync(int id);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task UpdateAsync(Genre genre);
    }
}
