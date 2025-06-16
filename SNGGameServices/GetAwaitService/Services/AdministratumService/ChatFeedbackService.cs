using GetAwaitService.Services.ChatFeedbackService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using System.Collections;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Services.ChatFeedbackService
{
    public class ChatFeedbackService : IChatFeedbackService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ChatFeedbackService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AdministratumServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<ChatFeedbackDTO>?> GetAll()
        {
            var resp = await _httpClient.GetAsync("api/ChatFeedback/GetAll");
            if (!resp.IsSuccessStatusCode) return null;
            var content = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ChatFeedbackDTO>>(content, _jsonOptions);
        }

        public async Task<ChatFeedbackDTO?> GetById(Guid id)
        {
            var resp = await _httpClient.GetAsync($"api/ChatFeedback/GetById/{id}");
            if (!resp.IsSuccessStatusCode) return null;
            var content = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ChatFeedbackDTO>(content, _jsonOptions);
        }

        public async Task<ChatFeedbackDTO?> Create(ChatFeedbackDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync("api/ChatFeedback/Create", content);
            if (!resp.IsSuccessStatusCode) return null;

            var respBody = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ChatFeedbackDTO>(respBody, _jsonOptions);
        }

        public async Task<bool> Update(ChatFeedbackDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/ChatFeedback/Update/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/ChatFeedback/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
