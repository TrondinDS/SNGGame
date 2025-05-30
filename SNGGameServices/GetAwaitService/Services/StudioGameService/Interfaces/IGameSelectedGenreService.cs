using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface IGameSelectedGenreService
    {
        Task<IEnumerable<GameSelectedGenreDTO>?> GetAllAsync();
        Task<GameSelectedGenreDTO?> GetByIdAsync(Guid id);
        Task<GameSelectedGenreDTO?> CreateAsync(GameSelectedGenreDTO dto);
        Task<bool> UpdateAsync(GameSelectedGenreDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
