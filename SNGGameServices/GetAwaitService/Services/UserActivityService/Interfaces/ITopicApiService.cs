using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;

namespace GetAwaitService.Services.UserActivityService.Interfaces
{
    public interface ITopicApiService
    {
        Task<IEnumerable<TopicDTO>?> GetAllAsync();
        Task<TopicDTO?> GetByIdAsync(Guid id);
        Task<TopicDTO?> CreateAsync(TopicDTO dto);
        Task<bool> UpdateAsync(Guid id, TopicDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
