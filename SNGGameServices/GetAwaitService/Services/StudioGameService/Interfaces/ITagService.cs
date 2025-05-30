using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDTO>?> GetAllAsync();
        Task<TagDTO?> GetByIdAsync(Guid id);
        Task<TagDTO?> CreateAsync(TagDTO dto);
        Task<bool> UpdateAsync(TagDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
