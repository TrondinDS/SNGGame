using System.Text;
using System.Text.Json;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Generics.DB.DTO.DTOModelView.UserActivityService.Topic;

namespace FrontService.Services.TopicService
{
    public class TopicApiService : ITopicApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TopicApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<TopicDTO>?> GetAllTopicsAsync()
        {
            var response = await _httpClient.GetAsync("api/Topic/GetAllTopics");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TopicDTO>>(body, _jsonOptions);
        }

        public async Task<TopicDTO?> GetTopicByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Topic/GetTopicById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TopicDTO>(body, _jsonOptions);
        }

        public async Task<IEnumerable<TopicDTOView>?> GetTopicsByEntityIdAsync(List<Guid> entityIds)
        {
            var content = new StringContent(JsonSerializer.Serialize(entityIds), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Topic/GetTopicByEntityId", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TopicDTOView>>(body, _jsonOptions);
        }

        public async Task<TopicDTO?> CreateTopicAsync(TopicCreateDTO topicDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(topicDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Topic/CreateTopic", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TopicDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateTopicAsync(Guid id, TopicDTO topicDto)
        {
            if (id != topicDto.Id) return false;

            var content = new StringContent(JsonSerializer.Serialize(topicDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Topic/UpdateTopic/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при обновлении темы. Статус: {response.StatusCode}, Детали: {errorContent}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTopicAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Topic/DeleteTopic/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
