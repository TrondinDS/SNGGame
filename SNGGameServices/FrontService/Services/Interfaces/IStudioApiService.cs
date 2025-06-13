using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;

namespace FrontService.Services.Interfaces
{
    public interface IStudioApiService
    {
        Task<IEnumerable<StudioDTO>?> GetAllStudiosAsync();
        Task<StudioDTO?> GetStudioByIdAsync(Guid id);
        Task<StudioDTO?> GetStudioByUserIdAsync(Guid userId);
        Task<StudioDTO?> CreateStudioAsync(StudioCreateDTO dto);
        Task<bool> UpdateStudioAsync(StudioDTO dto);
        Task<bool> DeleteStudioAsync(Guid id);
    }
}
