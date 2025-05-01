using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface ITagService
    {
        Task AddAsync(Tag tag);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(Guid id);
        Task UpdateAsync(Tag tag);
    }
}
