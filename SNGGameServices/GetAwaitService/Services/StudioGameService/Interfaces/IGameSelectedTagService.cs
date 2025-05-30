using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface IGameSelectedTagService
    {
        Task<IEnumerable<GameSelectedTagDTO>?> GetAllAsync();
        Task<GameSelectedTagDTO?> GetByIdAsync(Guid id);
        Task<GameSelectedTagDTO?> CreateAsync(GameSelectedTagDTO dto);
        Task<bool> UpdateAsync(GameSelectedTagDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
