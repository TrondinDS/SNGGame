using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface ITagService
    {
        Task AddAsync(Tag tag);
        Task DeleteAsync(int id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
        Task UpdateAsync(Tag tag);
    }
}
