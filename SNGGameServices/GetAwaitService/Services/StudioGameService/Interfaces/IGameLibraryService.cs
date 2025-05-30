using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface IGameLibraryService
    {
        Task<IEnumerable<GameLibraryDTO>?> GetAllAsync();
        Task<GameLibraryDTO?> GetByIdAsync(Guid id);
        Task<GameLibraryDTO?> CreateAsync(GameLibraryDTO dto);
        Task<bool> UpdateAsync(GameLibraryDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
