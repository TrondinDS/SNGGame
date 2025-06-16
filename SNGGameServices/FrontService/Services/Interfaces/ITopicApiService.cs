using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Generics.DB.DTO.DTOModelView.UserActivityService.Topic;

namespace FrontService.Services.Interfaces
{
    public interface ITopicApiService
    {
        Task<IEnumerable<TopicDTO>?> GetAllTopicsAsync();
        Task<TopicDTO?> GetTopicByIdAsync(Guid id);
        Task<IEnumerable<TopicDTOView>?> GetTopicsByEntityIdAsync(List<Guid> entityIds);
        Task<TopicDTO?> CreateTopicAsync(TopicCreateDTO topicDto);
        Task<bool> UpdateTopicAsync(Guid id, TopicDTO topicDto);
        Task<bool> DeleteTopicAsync(Guid id);
    }
}
