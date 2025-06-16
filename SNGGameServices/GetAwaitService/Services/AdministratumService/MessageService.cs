using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using System.Text.Json;
using System.Text;
using GetAwaitService.Services.ChatFeedbackService.Interfaces;

namespace GetAwaitService.Services.AdministratumService
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MessageService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AdministratumServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<MessageDTO>?> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Message/GetAll");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<MessageDTO>>(_jsonOptions);
        }

        public async Task<MessageDTO?> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Message/GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<MessageDTO>(_jsonOptions);
        }

        public async Task<MessageDTO?> Create(MessageDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Message/Create", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<MessageDTO>(_jsonOptions);
        }

        public async Task<bool> Update(MessageDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Message/Update/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Message/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
