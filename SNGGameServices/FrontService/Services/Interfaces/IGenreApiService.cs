using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;

namespace FrontService.Services.Interfaces
{
    public interface IGenreApiService
    {
        Task<IEnumerable<GenreDTO>?> GetAllGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(Guid id);
        Task<GenreDTO?> CreateGenreAsync(GenreCreateDTO genreDto);
        Task<bool> UpdateGenreAsync(Guid id, GenreDTO genreDto);
        Task<bool> DeleteGenreAsync(Guid id);
    }
}
