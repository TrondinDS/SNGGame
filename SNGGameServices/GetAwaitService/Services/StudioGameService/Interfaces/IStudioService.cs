using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface IStudioService
    {
        Task<IEnumerable<StudioDTO>?> GetAllAsync();
        Task<StudioDTO?> GetByIdAsync(Guid id);
        Task<StudioDTO?> CreateAsync(StudioDTO dto);
        Task<bool> UpdateAsync(StudioDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
