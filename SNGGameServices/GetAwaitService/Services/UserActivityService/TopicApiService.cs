using GetAwaitService.Services.UserActivityService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserActivityService
{
    public class TopicApiService : ITopicApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TopicApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserActivityServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<TopicDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Topic/GetAllTopics");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TopicDTO>>(body, _jsonOptions);
        }

        public async Task<TopicDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Topic/GetTopicById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TopicDTO>(body, _jsonOptions);
        }

        public async Task<TopicDTO?> CreateAsync(TopicDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Topic/CreateTopic", content);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TopicDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(Guid id, TopicDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Topic/UpdateTopic/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Topic/DeleteTopic/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
