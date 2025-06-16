using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Topic
{
    public class IndexModel : PageModel
    {
        private readonly ITopicApiService _topicApiService;

        public IndexModel(ITopicApiService topicApiService)
        {
            _topicApiService = topicApiService;
        }

        /// <summary>
        /// Список всех топиков, который будет отображаться на странице.
        /// </summary>
        public IEnumerable<TopicDTO> Topics { get; private set; } = Enumerable.Empty<TopicDTO>();

        /// <summary>
        /// Загружаем список топиков при открытии страницы.
        /// </summary>
        public async Task OnGetAsync()
        {
            var topics = await _topicApiService.GetAllTopicsAsync();
            Topics = topics ?? Enumerable.Empty<TopicDTO>();
        }
    }
}
