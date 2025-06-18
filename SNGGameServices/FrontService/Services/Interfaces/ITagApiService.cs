using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;

namespace FrontService.Services.Interfaces
{
    public interface ITagApiService
    {
        Task<IEnumerable<TagDTO>?> GetAllTagsAsync();
        Task<TagDTO?> GetTagByIdAsync(Guid id);
        Task<TagDTO?> CreateTagAsync(TagCreateDTO tagDto);
        Task<bool> UpdateTagAsync(Guid id, TagDTO tagDto);
        Task<bool> DeleteTagAsync(Guid id);
    }
}
