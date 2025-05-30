using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag;
using System.Text.Json;
using System.Text;
using GetAwaitService.Services.StudioGameService.Interfaces;

namespace GetAwaitService.Services.StudioGameService
{
    public class GameSelectedTagApiService : IGameSelectedTagService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameSelectedTagApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<GameSelectedTagDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/GameSelectedTag/GetAllGameSelectedTags");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameSelectedTagDTO>>(json, _jsonOptions);
        }

        public async Task<GameSelectedTagDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/GameSelectedTag/GetGameSelectedTagById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameSelectedTagDTO>(json, _jsonOptions);
        }

        public async Task<GameSelectedTagDTO?> CreateAsync(GameSelectedTagDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameSelectedTag/CreateGameSelectedTag", content);
            if (!response.IsSuccessStatusCode) return null;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameSelectedTagDTO>(jsonResponse, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(GameSelectedTagDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/GameSelectedTag/UpdateGameSelectedTag/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameSelectedTag/DeleteGameSelectedTag/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
