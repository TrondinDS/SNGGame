using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>?> GetAllAsync();
        Task<GenreDTO?> GetByIdAsync(Guid id);
        Task<GenreDTO?> CreateAsync(GenreDTO dto);
        Task<bool> UpdateAsync(GenreDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
