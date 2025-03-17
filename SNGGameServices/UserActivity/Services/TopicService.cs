using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;
using UserActivityService.Services.Interfaces;

namespace UserActivityService.Services
{
    public class TopicService : ITopicService
    {
        protected readonly ITopicRepository topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
        }

        public async Task AddAsync(Topic topic)
        {
            await topicRepository.AddAsync(topic);
            await topicRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = await topicRepository.GetByIdAsync(id);
            if (topic != null)
            {
                topicRepository.DeleteAsync(topic);
                await topicRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Topic>> GetAllAsync()
        {
            return topicRepository.GetAllAsync();
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            return await topicRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Topic topic)
        {
            topicRepository.UpdateAsync(topic);
            await topicRepository.SaveChangesAsync();
        }
    }
}
