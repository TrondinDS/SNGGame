using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGenreService
    {
        Task AddAsync(Genre genre);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(Guid id);
        Task UpdateAsync(Genre genre);
    }
}
