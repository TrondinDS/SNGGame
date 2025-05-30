using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;
using System.Text.Json;
using System.Text;
using GetAwaitService.Services.StudioGameService.Interfaces;

namespace GetAwaitService.Services.StudioGameService
{
    public class GameLibraryApiService : IGameLibraryService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameLibraryApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<GameLibraryDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/GameLibrary/GetAllGameLibraries");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameLibraryDTO>>(json, _jsonOptions);
        }

        public async Task<GameLibraryDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/GameLibrary/GetGameLibraryById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameLibraryDTO>(json, _jsonOptions);
        }

        public async Task<GameLibraryDTO?> CreateAsync(GameLibraryDTO dto)
        {
            var jsonContent = JsonSerializer.Serialize(dto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameLibrary/CreateGameLibrary", httpContent);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameLibraryDTO>(json, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(GameLibraryDTO dto)
        {
            var jsonContent = JsonSerializer.Serialize(dto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/GameLibrary/UpdateGameLibrary/{dto.Id}", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameLibrary/DeleteGameLibrary/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
